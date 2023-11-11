using System.Text.RegularExpressions;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Persistence.Concretes
{
    public class ValidationService : IValidationService
    {
       

        public EntityResultModel ValidateEmail(string email)
        {
            var entityResultModel = new EntityResultModel();
            if(string.IsNullOrEmpty(email)){entityResultModel.Result = EntityResult.Error; entityResultModel.ResultMessage = "Email adresi boş olamaz."; return entityResultModel;}
            
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            var isValid = Regex.IsMatch(email,pattern);
            if(!isValid)
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
            var entityResultModel = new EntityResultModel();
            bool checkLenght = password.Length >= 6;
            if(!checkLenght)
            {
                entityResultModel.Result = EntityResult.Error;
                entityResultModel.ResultMessage = "Parola en az 6 karakter olmalıdır.";
                return entityResultModel;
            }
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
    }
}