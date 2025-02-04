using BookSale.Application.Services.EmailSend;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Infrastructure.CommonService
{
    public class EmailTemplateRender : IEmailTemplateRender
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailTemplateRender(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> GetTemplate(string templateName)
        {
  
            string applicationRoot = Path.Combine(Directory.GetParent(_webHostEnvironment.ContentRootPath)
                                                                        .FullName, 
                                                                        "BookSale.Application");
            string templateEmail = Path.Combine(applicationRoot, templateName);
            string content = await File.ReadAllTextAsync(templateEmail);
            return content;
        }
    }
}
