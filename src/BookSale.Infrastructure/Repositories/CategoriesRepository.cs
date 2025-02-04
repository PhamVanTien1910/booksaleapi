using AutoMapper;
using BookSale.Application.Repositories;
using BookSale.Domain.Entities;
using BookSale.Infrastructure.Migrations.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Infrastructure.Repositories
{
    public class CategoriesRepository : BaseRepository<Categories>, ICategoriesRepository
    {
        public CategoriesRepository(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
        }
    }
}
