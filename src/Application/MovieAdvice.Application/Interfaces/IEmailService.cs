using MovieAdvice.Application.Dtos.Email;

namespace MovieAdvice.Application.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailDto emailDto);
    }
}
