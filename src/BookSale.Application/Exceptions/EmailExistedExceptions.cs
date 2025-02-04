using BookSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BookSale.Application.Exceptions
{
    public class EmailExistedExceptions : CustomExceptions
    {
        public EmailExistedExceptions() : base(StatusCodes.Status409Conflict, ErrorCodeEnums.ExistedEmail, "A book with the same title already exists.")
        {
        }
    }
}
