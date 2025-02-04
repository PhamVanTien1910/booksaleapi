using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Books
{
    public class BookTitleSpecification(string title) : Specification<Book>
    {
        public override Expression<Func<Book, bool>> ToExpression()
        {
            return book => book.Title == title;
        }
    }
}
