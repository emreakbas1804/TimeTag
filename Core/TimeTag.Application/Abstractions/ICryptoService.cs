namespace TimeTag.Application.Abstractions;
public interface ICryptoService
{
    string HashPassword(string password);
    string GenerateRandomPassword();
}