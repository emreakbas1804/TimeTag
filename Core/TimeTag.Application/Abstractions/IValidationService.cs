using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IValidationService
{
    EntityResultModel ValidateEmail(string email);
    EntityResultModel ValidatePassword(string password);
}