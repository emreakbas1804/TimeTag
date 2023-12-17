using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IEmailService
{
    Task<EntityResultModel> SendMail(string email, string subject, string bodyHtml);
    string GetChangePasswordEmailSchema(string userName, string code);
}