using dotnet_boilerplate.Domain.Entities;
using BookSale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Users
{
    public class UserRoleIdSpecification : Specification<User>
    {
        private readonly RoleEnums _roleEnums;

        public UserRoleIdSpecification(RoleEnums roleEnums)
        {
            _roleEnums = roleEnums;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.RoleId == (int)_roleEnums;
        }
    }
}
