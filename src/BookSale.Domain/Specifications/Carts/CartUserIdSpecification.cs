using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Carts
{
    public class CartUserIdSpecification(int id) : Specification<Cart>
    {
        public override Expression<Func<Cart, bool>> ToExpression()
        {
            return cart => cart.UserId == id;
        }
    }
}
