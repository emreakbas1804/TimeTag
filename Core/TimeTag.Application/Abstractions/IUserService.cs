using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;

namespace TimeTag.Application.Abstractions;
public interface IUserService 
{
    Task<User> GetUserByLogin(string email,string password);    
    bool IsUserExistByEmail(string email);
    bool IsPhoneExist(string phone);
    Task<EntityResultModel> AddUser(RegisterDTO model);
    void SetSessionUser(User userEntity);
    User GetSessionUser();
    EntityResultModel GenerateToken(User userEntity, string role);
    void AddLoginLog(int userId, bool isSuccessLogin);
}
