using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IValidationService
{
   Task<EntityResultModel> ValidateEmailAsync(string email);
    Task<EntityResultModel> ValidatePassword(string password);
    Task<EntityResultModel> ValidateFileSizeAsync(IFormFile file,int fileSizeMb);
    Task<EntityResultModel> ValidateFileExtensionAsync(IFormFile file, string[] acceptExtensions);
}