using Microsoft.AspNetCore.Http;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IValidationService
{
    EntityResultModel ValidateEmail(string email);
    EntityResultModel ValidatePassword(string password);
    EntityResultModel ValidateFileSize(IFormFile file,int fileSizeMb);
    EntityResultModel ValidateFileExtension(IFormFile file, string[] acceptExtensions);
}