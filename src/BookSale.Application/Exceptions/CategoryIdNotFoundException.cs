using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class CategoryIdNotFoundException : NotFoundExceptions
    {
        public CategoryIdNotFoundException() : base("The category Id dose not exits")
        {
        }
    }
}
