using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Entities;
using TimeTag.Domain.Enums;
using TimeTag.Persistence.Context;

namespace TimeTag.Persistence.Concretes;
public class FileService : IFileService
{
    private readonly EntityDbContext _context;
    private readonly IValidationService _validationService;
    public FileService(EntityDbContext context, IValidationService validationService)
    {
        _context = context;
        _validationService = validationService;
    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> UploadFile(IFormFile file, string mainPath, int maxFileSizeMb, string[] acceptExtensions)
    {
        FileUpload fileUpload;
        try
        {

            if (file == null)
            {
                entityResultModel.ResultMessage = "Dosya alınamadı.";
                return entityResultModel;
            }
            if (string.IsNullOrEmpty(mainPath))
            {
                entityResultModel.ResultMessage = "Dosya kayıt yolu bulunamadı.";
                return entityResultModel;
            }

            var validateFileSize = _validationService.ValidateFileSize(file, maxFileSizeMb);
            if (validateFileSize.Result != EntityResult.Success) return validateFileSize;

            var validateFileExtension = _validationService.ValidateFileExtension(file, acceptExtensions);
            if (validateFileExtension.Result != EntityResult.Success) return validateFileExtension;



            var extension = Path.GetExtension(file?.FileName);
            string guid = Guid.NewGuid().ToString();
            var path = mainPath + guid + extension;
            var fileSizeMB = (file?.Length ?? 0) / (1024.0 * 1024.0);
            
            using (var stream = new FileStream("wwwroot/"+path, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
            fileUpload = new()
            {
                FileUrl = path,
                OriginalFileName = file?.FileName,
                FileSize = fileSizeMB.ToString()
            };
            await _context.FileUploads.AddAsync(fileUpload);
            await _context.SaveChangesAsync();
            entityResultModel.Id = fileUpload.Id;
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
            return entityResultModel;
        }


    }
}