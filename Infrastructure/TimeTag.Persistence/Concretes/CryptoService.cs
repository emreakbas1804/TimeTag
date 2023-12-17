using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TimeTag.Application.Abstractions;

namespace TimeTag.Persistence.Concretes;
public class CryptoService : ICryptoService
{
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) return "";
        using (SHA256 sha256Hash = SHA256.Create())
        {

            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);
            string hash = Convert.ToBase64String(hashBytes);
            return hash;
        }
    }

    
    public string GenerateRandomPassword()
    {
        Random random = new();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 6)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }


}