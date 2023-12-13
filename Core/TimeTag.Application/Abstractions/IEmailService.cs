using System;
using System.Threading.Tasks;
using TimeTag.Application.DTO;

namespace TimeTag.Application.Abstractions;
public interface IEmailService
{
    Task<EntityResultModel> SendMail(string email, string subject, string bodyHtml);
    string GetForgotPasswordEmailSchema(string userName, string code);
}