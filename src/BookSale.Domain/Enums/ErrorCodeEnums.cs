using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Enums
{
    public enum ErrorCodeEnums
    {
        InvalidRequest = 101,
        IncorrectEmailOrPassword = 102,
        ServerError = 103,
        InvalidSyntax = 104,
        ExistedEmail = 108,
        NotLogedIn = 116,
        NotFound = 115,
        NotAuthorized = 113,
        ExistedBook = 109,
    }
}
