using BookSale.Application.Dtos.Request;

namespace BookSale.Application.EmailHelper
{
    public interface IEmailHelper
    {
        Task SendEmail(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}