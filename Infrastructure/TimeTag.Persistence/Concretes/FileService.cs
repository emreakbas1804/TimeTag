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
    private readonly ILocalizationService _localizationService;
    public FileService(EntityDbContext context, IValidationService validationService, ILocalizationService localizationService)
    {
        _context = context;
        _validationService = validationService;
        _localizationService = localizationService;
    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> UploadFile(IFormFile file, string mainPath, int maxFileSizeMb, string[] acceptExtensions)
    {
        FileUpload fileUpload;
        try
        {

            if (file == null)
            {                
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_dosya_alinamadi", "File could not be retrieved");

                return entityResultModel;
            }
            if (string.IsNullOrEmpty(mainPath))
            {
                
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_dosya_kayit_yolu_bulunamadi", "File save path not found");
                return entityResultModel;
            }

            var validateFileSize =await _validationService.ValidateFileSizeAsync(file, maxFileSizeMb);
            if (validateFileSize.Result != EntityResult.Success) return validateFileSize;

            var validateFileExtension =await _validationService.ValidateFileExtensionAsync(file, acceptExtensions);
            if (validateFileExtension.Result != EntityResult.Success) return validateFileExtension;



            var extension = Path.GetExtension(file?.FileName);
            string guid = Guid.NewGuid().ToString();
            var path = mainPath + guid + extension;
            var fileSizeMB = (file?.Length ?? 0) / (1024.0 * 1024.0);

            using (var stream = new FileStream("wwwroot/" + path, FileMode.CreateNew))
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
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_beklenmedik_bir_hata_olustu", "Unknow error. Please try again later");

            return entityResultModel;
        }


    }
}