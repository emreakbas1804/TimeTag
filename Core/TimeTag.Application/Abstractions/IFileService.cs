using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;

namespace TimeTag.Application.Abstractions;
public interface IFileService
{
    Task<EntityResultModel> UploadFile(IFormFile file, string mainPath, int maxFileSizeMb, string[] acceptExtensions);
}

