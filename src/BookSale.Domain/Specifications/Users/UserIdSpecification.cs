using dotnet_boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Users
{
    public class UserIdSpecification(int id) : Specification<User>
    {
        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Id == id;
        }
    }
}
