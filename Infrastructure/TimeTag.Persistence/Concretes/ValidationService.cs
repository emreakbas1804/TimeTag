using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Persistence.Concretes
{
    public class ValidationService : IValidationService
    {
        private readonly ILocalizationService _localizationService;
        public ValidationService(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        EntityResultModel entityResultModel = new();
        public async Task<EntityResultModel> ValidateEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email)) { entityResultModel.Result = EntityResult.Error; entityResultModel.ResultMessage =await _localizationService.getLocalization("txt_email_adresi_bos_olamaz","Email address can't be empty"); return entityResultModel; }

            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            var isValid = Regex.IsMatch(email, pattern);
            if (!isValid)
            {
                entityResultModel.Result = EntityResult.Error;                
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_gecersiz_email_adresi", "Invalid email address");

                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }

        public async Task<EntityResultModel> ValidatePassword(string password)
        {
            bool checkLenght = password?.Length >= 6;
            if (!checkLenght)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_parola_en_az_6_karakter_olmalidir","Password must be at least 6 characters");
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }


        public async Task<EntityResultModel> ValidateFileSizeAsync(IFormFile file, int fileSizeMb)
        {
            try
            {
                long maxSize = fileSizeMb * 1024 * 1024;
                if (file?.Length > maxSize)
                {
                    entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_dosya_boyutu_buyuk",$"File size connot be than {fileSizeMb}");
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

        public async Task<EntityResultModel> ValidateFileExtensionAsync(IFormFile file, string[] acceptExtensions)
        {
            try
            {
                var fileExtension = Path.GetExtension(file?.FileName);
                if (!acceptExtensions.Contains(fileExtension))
                {
                    entityResultModel.ResultMessage = "Geçersiz dosya uzantısı.";
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


    }
}