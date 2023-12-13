using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TimeTag.Application.Abstractions;
using TimeTag.Application.DTO;
using TimeTag.Domain.Enums;

namespace TimeTag.Persistence.Concretes;
public class EmailService : IEmailService
{
    private readonly ILocalizationService _localizationService;
    public EmailService(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
    }
    EntityResultModel entityResultModel = new();
    public async Task<EntityResultModel> SendMail(string email, string subject, string bodyHtml)
    {
        try
        {
            string fromAddress = "info@ossdoy.com";
            string password = "emre.Akbas1804";

            // SmtpClient oluştur
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(fromAddress, password);            
            smtpClient.Host = "rd-prime-win.guzelhosting.com";
            smtpClient.EnableSsl = false;


            // E-posta oluştur
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddress);
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = bodyHtml;
            mailMessage.IsBodyHtml = true;

            smtpClient.Send(mailMessage);
            entityResultModel.Result = EntityResult.Success;
            return entityResultModel;
        }
        catch (System.Exception)
        {
            entityResultModel.ResultMessage = await _localizationService.getLocalization("txt_email_gonderilemedi", "Email could not sent");
            return entityResultModel;
        }
    }

    public string GetForgotPasswordEmailSchema(string userName, string code)
    {
        // HTML e-posta gövdesi
        string emailBody = $@"<!DOCTYPE html>
            <html lang='tr'>
            <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Parola Sıfırlama</title>
            <style>
                body {{
                    font-family: 'Arial', sans-serif;
                    background-color: #fff;
                    color: #333;
                    margin: 0;
                    padding: 0;
                }}
                .container {{
                    max-width: 600px;
                    margin: 20px auto;
                    padding: 20px;
                    border: 1px solid #ddd;
                    border-radius: 5px;
                }}
                h1 {{
                    color: #316fc9;
                }}
                p {{
                    line-height: 1.6;
                }}
                .code {{
                    background-color: #316fc9;
                    color: #fff;
                    padding: 10px;
                    border-radius: 5px;
                    font-size: 18px;
                    margin-top: 15px;
                    text-align: center;
                }}
            </style>
            </head>
            <body>
            <div class='container'>
                <h1>Forgot Password</h1>
                <p>Hello, {userName}</p>
                <p>You can use the following 6-digit code to reset your password</p>
                <div class='code'>{code}</div>                
                <p>Regards,<br>Time Tag</p>
            </div>
            </body>
            </html>
            ";



        return emailBody;
    }
}
