using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class UserEmailNotFoundException : NotFoundExceptions
    {
        public UserEmailNotFoundException() : base("The user email dose not exits")
        {
        }
    }
}
