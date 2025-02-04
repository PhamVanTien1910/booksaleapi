using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Authors
{
    public class AuthorIdSpecification(int id) : Specification<BookSale.Domain.Entities.Authors>
    {
        public override System.Linq.Expressions.Expression<Func<BookSale.Domain.Entities.Authors, bool>> ToExpression()
        {
            return authors => authors.Id == id;
        }
    }
}
