using BookSale.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class BookTitleExistedException : CustomExceptions
    {
        public BookTitleExistedException() : base(StatusCodes.Status409Conflict, ErrorCodeEnums.ExistedBook, "The book title already exists in the system.")
        {
        }
    }
}
