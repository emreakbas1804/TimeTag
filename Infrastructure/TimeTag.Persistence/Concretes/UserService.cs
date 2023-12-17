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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TimeTag.Persistence.Concretes;
public class UserService : IUserService
{
    private readonly EntityDbContext _context;
    private readonly ICryptoService _cryptoService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ILocalizationService _localizationService;
    private readonly IEmailService _emailService;
    EntityResultModel entityResultModel = new();
    public UserService(EntityDbContext context, ICryptoService cryptoService, IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ILocalizationService localizationService, IEmailService emailService)
    {
        _context = context;
        _cryptoService = cryptoService;
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _localizationService = localizationService;
        _emailService = emailService;
    }

    public async Task<EntityResultModel> AddUser(RegisterDTO model)
    {
        try
        {
            bool isUserExist = IsUserExistByEmail(model.Email);
            if (isUserExist)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_mail_adresi_sistemimizde_kayitli", "Email address registered in our system");
                return entityResultModel;
            }
            bool isPhoneExist = IsPhoneExist(model.Phone);
            if (isPhoneExist)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_telefon_numarasi_sistemimizde_kayitli", "Phone number registered in our system");
            }
            model.Password = _cryptoService.HashPassword(model.Password);
            User user = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Phone = model.Phone,
                Password = model.Password,
                IsFirstLogin = model.IsFirstLogin,
                rlt_Role_Id = _context.Roles.Where(q => q.IsSystemRole).Select(c => c.Id).FirstOrDefault()
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            entityResultModel.Result = EntityResult.Success;
            entityResultModel.Id = user.Id;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
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
                if (userEntity.IsFirstLogin)
                {                                        
                    entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_parola_degistirmelisiniz", "You must change the your password");
                    entityResultModel.Result = EntityResult.Warning;
                    return entityResultModel;
                }
                if (userEntity.Password == password)
                {
                    AddLoginLog(userEntity.Id, true);
                    string role = await _context.Users.Where(q => q.Email == email).Select(c => c.Role.Name).FirstOrDefaultAsync() ?? "User";
                    return await GenerateTokenAsync(userEntity, role);
                }
                else
                {
                    AddLoginLog(userEntity.Id, false);
                    entityResultModel.ResultMessage = "Mail adresi veya parola hatalı";
                    entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_mail_adresi_veya_parola_hatali", "Email address or password is incorrect");
                    return entityResultModel;
                }
            }
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_mail_adresi_bulunamadi", "Email address not found");
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
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

    public async Task<EntityResultModel> GenerateTokenAsync(User userEntity, string role)
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
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
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
            _context.User_LoginLogs.Add(loginLog);
            _context.SaveChanges();
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

    public async Task<EntityResultModel> UpdateUser(int userId, string email, string phone, string password)
    {
        try
        {
            var userEntity = await _context.Users.Where(q => q.Id == userId).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_kullanici_bulunamadi", "User not found");
                return entityResultModel;
            }

            userEntity.Email = email;
            userEntity.Phone = phone;
            if (!string.IsNullOrEmpty(password))
            {
                userEntity.Password = _cryptoService.HashPassword(password);
            }
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
            return entityResultModel;
        }

    }

    public async Task<EntityResultModel> GetUserProfile(int userId)
    {
        try
        {
            var userEntity = await _context.Users.Where(q => q.Id == userId).Select(c => new
            {
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                Phone = c.Phone,
                Password = ""
            }).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_kullanici_bulunamadi", "User not found");
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            entityResultModel.ResultObject = userEntity;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
            return entityResultModel;
        }

    }

    public async Task<EntityResultModel> AddContactMessage(string nameSurname, string phone, string email, string message)
    {
        try
        {
            Contact contactMessage = new()
            {
                Email = email,
                NameSurname = nameSurname,
                Phone = phone,
                Message = message
            };
            await _context.Contacts.AddAsync(contactMessage);
            await _context.SaveChangesAsync();
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
            return entityResultModel;
        }
    }

    public async Task<EntityResultModel> ForgotPassword(string email)
    {
        try
        {
            var userEntity = await _context.Users.Where(q => q.Email == email).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_kullanici_bulunamadi", "User not found");
                return entityResultModel;
            }


            Random random = new();
            var code = random.Next(100000, 1000000);
            SecurityCode securityCode = new()
            {
                ProcessTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMinutes(45),
                Email = email,
                Code = code,
                Type = SecurityCodeType.ChangePassword,
                rlt_User_Id = userEntity.Id
            };
            await _context.SecurityCodes.AddAsync(securityCode);
            await _context.SaveChangesAsync();
            // email gönderilecek
            var schema = _emailService.GetChangePasswordEmailSchema(userEntity.Name + " " + userEntity.Surname, code.ToString());
            var sendEmail = await _emailService.SendMail(email, "Change Password", schema);
            if (sendEmail.Result != EntityResult.Success)
            {
                entityResultModel.ResultMessage = sendEmail.ResultMessage;
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
            return entityResultModel;
        }
    }

    public async Task<EntityResultModel> ResetPassword(string email, string code, string password)
    {
        try
        {
            var userEntity = await _context.Users.Where(q => q.Email == email).FirstOrDefaultAsync();
            if (userEntity == null)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_kullanici_bulunamadi", "User not found");
            }

            var securityCode = await _context.SecurityCodes.Where(q => q.Code.ToString() == code && q.Email == email && q.Type == SecurityCodeType.ChangePassword).FirstOrDefaultAsync();
            if (securityCode == null)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_dogrulama_kodu_hatali", "Verification code is incorrect");
                return entityResultModel;
            }
            if (securityCode.ExpiryDate < DateTime.Now)
            {
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_dogrulama_kodu_zaman_asimina_ugradi", "Verification code has timed out.");
                return entityResultModel;
            }

            userEntity.Password = _cryptoService.HashPassword(password);
            userEntity.LastUpdateTime = DateTime.Now;
            userEntity.IsFirstLogin = false;
            _context.Users.Update(userEntity);
            await _context.SaveChangesAsync();

            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");
            return entityResultModel;
        }
    }

    public async Task<int> GetUserIdByEmail(string email)
    {
        return await _context.Users.Where(c => c.Email == email).Select(q => q.Id).FirstOrDefaultAsync();
    }

}
