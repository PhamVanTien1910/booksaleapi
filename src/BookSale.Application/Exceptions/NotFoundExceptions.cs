using BookSale.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class NotFoundExceptions : CustomExceptions
    {
        public NotFoundExceptions(string message) : base(StatusCodes.Status404NotFound, ErrorCodeEnums.NotFound, message)
        {
        }
    }
}
