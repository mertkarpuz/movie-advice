using Microsoft.Extensions.Hosting;
using MovieAdvice.Application.ConfigModels;
using MovieAdvice.Application.Dtos.Email;
using MovieAdvice.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace MovieAdvice.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly Configuration configuration;
        private readonly HostBuilderContext environment;

        public EmailService(Configuration configuration, HostBuilderContext environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public Task<bool> SendEmailAsync(EmailDto emailDto)
        {
            var result = false;
            using (var message = new MailMessage())
            {
                message.To.Add(new MailAddress(emailDto.To));
                message.From = new MailAddress(configuration.EmailConfiguration.From, configuration.EmailConfiguration.FromName);
                message.Subject = configuration.EmailConfiguration.Subject;
                message.Body = ChangeMailTemplate(emailDto.Sender, emailDto.MovieImage, emailDto.MovieTitle,emailDto.MovieDescription);
                message.IsBodyHtml = true;
                using (var client = new SmtpClient(configuration.EmailConfiguration.Client))
                {
                    client.Port = configuration.EmailConfiguration.Port;
                    client.Credentials = new NetworkCredential(configuration.EmailConfiguration.UserName, configuration.EmailConfiguration.Password);
                    client.EnableSsl = configuration.EmailConfiguration.Ssl;
                    client.Send(message);
                }
                result = true;
            }
            return Task.FromResult(result);
        }

        private string ChangeMailTemplate(string userName, string movieImage, string movieTitle, string movieDescription)
        {
            string body = string.Empty;
            var path = environment.HostingEnvironment.ContentRootPath;
            using var reader = new StreamReader(path + "/wwwroot/MailTemplate/mail-template.txt");
            string readFile = reader.ReadToEnd();
            string StrContent = string.Empty;
            StrContent = readFile;
            StrContent = StrContent.Replace("[FullName]", userName);
            StrContent = StrContent.Replace("[MovieImage]", movieImage);
            StrContent = StrContent.Replace("[MovieTitle]", movieTitle);
            StrContent = StrContent.Replace("[MovieDescription]", movieDescription);
            body = StrContent.ToString();
            return body;
        }

    }
}
