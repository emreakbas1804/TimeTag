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

namespace TimeTag.Persistence.Concretes;
public class UserService : IUserService
{
    private readonly EntityDbContext _context;
    private readonly ICryptoService _cryptoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public UserService(EntityDbContext context, ICryptoService cryptoService, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _cryptoService = cryptoService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EntityResultModel> AddUser(RegisterDTO model)
    {
        EntityResultModel entityResultModel = new();

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
            Password = model.Password
        };
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        entityResultModel.Result = EntityResult.Success;
        return entityResultModel;
    }

    public async Task<User> GetUserByLogin(string email, string password)
    {
        User userEntity = new();
        password = _cryptoService.HashPassword(password);
        userEntity = await _context.Users.Where(q => q.Email == email && q.Password == password).FirstOrDefaultAsync();
        return userEntity;
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

    public void SetSessionUser(User userEntity)
    {
        _httpContextAccessor.HttpContext.Session.Set("CurrentUser", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userEntity)));
    }
    public User GetSessionUser()
    {
        User user = null;
        if (_httpContextAccessor.HttpContext.Session.TryGetValue("CurrentUser", out var sessionValue))
        {
            user = JsonConvert.DeserializeObject<User>(sessionValue.ToString());
        }
        return user;
    }

    public EntityResultModel GenerateToken(User userEntity, string role)
    {
        EntityResultModel entityResultModel = new();
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
            Expires = DateTime.UtcNow.AddMinutes(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenString = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor)); 
        
        dynamic tokenInfo = new
        {
            Token = tokenString,
            ExpiryDate = tokenDescriptor.Expires            
        };
        entityResultModel.ResultObject = tokenInfo;
        entityResultModel.Result = EntityResult.Success;
        return entityResultModel;
    }

    public async void AddLoginLog(int userId, bool IsSuccessLogin)
    {
        var ipAddress  =_httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        var referanceUrl =$"{_httpContextAccessor.HttpContext.Request.Scheme } ://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.Path}"; 
        var loginLog = new User_LoginLog()
        {
            IpAddress = ipAddress,
            IsSuccessLogin = IsSuccessLogin,
            ReferanceUrl = referanceUrl, 
            rlt_User_Id = userId           
        };
        await _context.User_LoginLogs.AddAsync(loginLog);
        await _context.SaveChangesAsync();
    }
}
