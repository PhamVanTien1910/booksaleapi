﻿using BookSale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Domain.Specifications.Categories
{
    public class CategoryIdSpecificationcs(int id) : Specification<BookSale.Domain.Entities.Categories>
    {
        public override System.Linq.Expressions.Expression<Func<BookSale.Domain.Entities.Categories, bool>> ToExpression()
        {
            return category => category.Id == id;
        }
    }
}
