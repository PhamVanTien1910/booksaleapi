using BookSale.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Exceptions
{
    public class RoleIdNotFoundException : NotFoundExceptions
    {
        public RoleIdNotFoundException() : base("The role ID does not exist")
        {
        }
    }
}
