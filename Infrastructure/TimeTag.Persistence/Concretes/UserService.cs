using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Configuration;
using System.Dynamic;

namespace TimeTag.Persistence.Concretes;
public class UserService : IUserService
{
    private readonly EntityDbContext _context;
    private readonly ICryptoService _cryptoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    EntityResultModel entityResultModel = new();
    public UserService(EntityDbContext context, ICryptoService cryptoService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _context = context;
        _cryptoService = cryptoService;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public async Task<EntityResultModel> AddUser(RegisterDTO model)
    {
        try
        {
            bool isUserExist = IsUserExistByEmail(model.Email);
            if (isUserExist)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = "Mail adresi sistemimizde kayıtlı.";
                return entityResultModel;
            }
            bool isPhoneExist = IsPhoneExist(model.Phone);
            if (isPhoneExist)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = "Telefon numarası sistemimizde kayıtlı.";
            }
            model.Password = _cryptoService.HashPassword(model.Password);
            User user = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password,
                rlt_Role_Id = _context.Roles.Where(q => q.IsSystemRole).Select(c => c.Id).FirstOrDefault()
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            entityResultModel.Result = EntityResult.Success;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu";
        }
        return entityResultModel;
    }

    public async Task<EntityResultModel> Login(string email, string password)
    {
        try
        {
            password = _cryptoService.HashPassword(password);
            var userEntity = await _context.Users.Where(q => q.Email == email).FirstOrDefaultAsync();
            if (userEntity != null)
            {
                if (userEntity.Password == password)
                {
                    AddLoginLog(userEntity.Id, true);
                    return GenerateToken(userEntity, "User");
                }
                else
                {
                    AddLoginLog(userEntity.Id, false);
                    entityResultModel.ResultMessage = "Mail adresi veya parola hatalı";
                    return entityResultModel;
                }
            }
            entityResultModel.ResultMessage = "Mail adresi bulunamadı";
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu";
        }

        return entityResultModel;
    }

    public bool IsPhoneExist(string phone)
    {
        var isPhoneExist = _context.Users.Any(q => q.Phone == phone);
        return isPhoneExist;
    }

    public bool IsUserExistByEmail(string email)
    {
        var isUserExist = _context.Users.Any(q => q.Email == email);
        return isUserExist;
    }

    public EntityResultModel GenerateToken(User userEntity, string role)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configurations.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("UserId", userEntity.Id.ToString()),
                    new Claim(ClaimTypes.Email, userEntity.Email),
                    new Claim(ClaimTypes.Name, userEntity.Name),
                    new Claim(ClaimTypes.Surname, userEntity.Surname),
                    new Claim(ClaimTypes.Role, role)
                    }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            dynamic tokenInfo = new
            {
                Token = tokenString,
                ExpiryDate = tokenDescriptor.Expires,
                FirstName = userEntity.Name,
                SurName = userEntity.Surname
            };
            entityResultModel.ResultObject = tokenInfo;
            entityResultModel.Result = EntityResult.Success;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu";
        }
        return entityResultModel;
    }

    public void AddLoginLog(int userId, bool IsSuccessLogin)
    {
        try
        {
            var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
            var referanceUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme} ://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}";
            var loginLog = new User_LoginLog()
            {
                IpAddress = ipAddress,
                IsSuccessLogin = IsSuccessLogin,
                ReferanceUrl = referanceUrl,
                rlt_User_Id = userId
            };
            _context.User_LoginLogs.AddAsync(loginLog);
            _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }

    }

    public CurrentUser GetCurrentUser(string jwtToken)
    {
        CurrentUser currentUser = new();
        try
        {
            if (string.IsNullOrEmpty(jwtToken))
                return currentUser;

            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
            };

            var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out _);

            currentUser.Id = int.Parse(principal.FindFirst("UserId")?.Value);
            currentUser.Email = principal.FindFirst(ClaimTypes.Email)?.Value;
            currentUser.Name = principal.FindFirst(ClaimTypes.Name)?.Value;
            currentUser.Surname = principal.FindFirst(ClaimTypes.Surname)?.Value;

        }
        catch (System.Exception)
        {
            throw;
        }

        return currentUser;
    }

}
