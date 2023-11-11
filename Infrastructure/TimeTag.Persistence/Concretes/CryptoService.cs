using System;
using System.Security.Cryptography;
using System.Text;
using TimeTag.Application.Abstractions;

namespace TimeTag.Persistence.Concretes;
public class CryptoService : ICryptoService
{
    public string HashPassword(string password)
    {
        if(string.IsNullOrEmpty(password)) return "";
        using (SHA256 sha256Hash = SHA256.Create())
        {

            byte[] bytes = Encoding.UTF8.GetBytes(password);    
            byte[] hashBytes = sha256Hash.ComputeHash(bytes);            
            string hash = Convert.ToBase64String(hashBytes);
            return hash;
        }
    }
}