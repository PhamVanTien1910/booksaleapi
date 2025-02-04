using BookSale.Application.Dtos.Request;

namespace DotnetBoilerplate.Core.EmailHelper
{
    public interface IEmailHelper
    {
        Task SendEmail(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}