using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class AuthorIdNotFoundException : NotFoundExceptions
    {
        public AuthorIdNotFoundException() : base("The author Id dose not exits")
        {
        }
    }
}
