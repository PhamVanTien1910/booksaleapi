namespace BookSale.Application.Services.EmailSend
{
    public interface IEmailTemplateRender
    {
        Task<string> GetTemplate(string templateName);
    }
}