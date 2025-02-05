using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class CartNotFoundException : NotFoundExceptions
    {
        public CartNotFoundException() : base("No items in the cart.")
        {
        }
    }
}
