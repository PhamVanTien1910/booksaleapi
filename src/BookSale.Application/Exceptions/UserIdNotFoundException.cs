using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class UserIdNotFoundException : NotFoundExceptions
    {
        public UserIdNotFoundException() : base("The user Id dose not exits")
        {

        }
    }
}
