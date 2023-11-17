using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Persistence.Concretes
{
    public class ValidationService : IValidationService
    {

        EntityResultModel entityResultModel = new();
        public EntityResultModel ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) { entityResultModel.Result = EntityResult.Error; entityResultModel.ResultMessage = "Email adresi boş olamaz."; return entityResultModel; }

            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            var isValid = Regex.IsMatch(email, pattern);
            if (!isValid)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = "Geçersiz bir email adresi";
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }

        public EntityResultModel ValidatePassword(string password)
        {
            bool checkLenght = password?.Length >= 6;
            if (!checkLenght)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = "Parola en az 6 karakter olmalıdır.";
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }


        public EntityResultModel ValidateFileSize(IFormFile file, int fileSizeMb)
        {
            try
            {
                long maxSize = fileSizeMb * 1024 * 1024;
                if (file?.Length > maxSize)
                {
                    entityResultModel.ResultMessage = $"Dosya boyutu {fileSizeMb} 'dan büyük olamaz.";
                    return entityResultModel;
                }
                entityResultModel.Result = EntityResult.Success;
                return entityResultModel;
            }
            catch (System.Exception)
            {
                entityResultModel.ResultMessage = "Beklenmedik bir hata oluştu.";
                return entityResultModel;
            }
        }

        public EntityResultModel ValidateFileExtension(IFormFile file, string[] acceptExtensions)
        {
            try
            {
                var fileExtension = Path.GetExtension(file.FileName);
                if(!acceptExtensions.Contains(fileExtension))
                {
                   entityResultModel.ResultMessage = "Geçersiz dosya uzantısı."; 
                   return entityResultModel;
                }
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
}