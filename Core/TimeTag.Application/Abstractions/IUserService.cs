using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;

namespace TimeTag.Application.Abstractions;
public interface IUserService 
{
    Task<EntityResultModel> Login(string email,string password);    
    bool IsUserExistByEmail(string email);
    bool IsPhoneExist(string phone);
    Task<EntityResultModel> AddUser(RegisterDTO model);
    Task<EntityResultModel> GenerateTokenAsync(User userEntity, string role);
    void AddLoginLog(int userId, bool isSuccessLogin);
    CurrentUser GetCurrentUser(string jwtToken);
    
}
