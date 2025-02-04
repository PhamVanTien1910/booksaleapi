using BookSale.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Application.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> FristOrDefaultAsync(
            Specification<T>? spec = null,
            List<Expression<Func<T, object>>>? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null
            );

        Task<TDto?> FirstOrDefaultAsync<TDto>(
            Specification<T>? spec = null,
            List<Expression<Func<T, object>>>? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByEmailAsync(string email);
        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> ExistsAsync(Specification<T>? spec = null);
        Task<int> CountAsync(Specification<T>? spec = null);
        Task<List<T>> ToListAsync(
        Specification<T>? spec = null,
        List<Expression<Func<T, object>>>? includes = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        int? page = null, int? size = null);

        Task<List<TDto>> ToListAsync<TDto>(
            Specification<T>? spec = null,
            List<Expression<Func<T, object>>>? includes = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? page = null, int? size = null);


    }
}
