using dotnet_boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Users
{
    public class UserEmailSpecification : Specification<User>
    {
        private readonly string _email;

        public UserEmailSpecification(string email)
        {
            _email = email;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
           return user => user.Email == _email;
        }
    }
}
