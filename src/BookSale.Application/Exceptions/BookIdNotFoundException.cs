using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class BookIdNotFoundException : NotFoundExceptions
    {
        public BookIdNotFoundException() : base("The book Id dose not exits")
        {
        }
    }
}
