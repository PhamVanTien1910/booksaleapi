using BookSale.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class AuthorizationException : CustomExceptions
    {
        public AuthorizationException(string message) : base(StatusCodes.Status403Forbidden, ErrorCodeEnums.NotAuthorized, message)
        {
        }
    }
}
