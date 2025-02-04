using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookSale.Application.Repositories;
using BookSale.Domain.Specifications;
using BookSale.Infrastructure.Migrations.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public readonly DataContext _dataContext;
        public readonly DbSet<T> _dbSet;
        public readonly IMapper _mapper;

        public BaseRepository(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _dbSet = dataContext.Set<T>();
            _mapper = mapper ;
        }


        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public Task<int> CountAsync(Specification<T>? spec = null)
        {
            return _dbSet.AsNoTracking().ApplySpecification(spec).CountAsync();
        }

        public  void Delete(T entity)
        {
             _dbSet.Remove(entity);
        }

        public Task<bool> ExistsAsync(Specification<T>? spec = null)
        {
            return _dbSet.AsNoTracking().ApplySpecification(spec).AnyAsync();
        }

        public Task<TDto?> FirstOrDefaultAsync<TDto>(Specification<T>? spec = null, List<Expression<Func<T, object>>>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return _dbSet.AsNoTracking()
              .ApplySpecification(spec)
              .ApplyOrder(orderBy)
              .ProjectTo<TDto>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync();
        }

        public Task<T?> FristOrDefaultAsync(Specification<T>? spec = null, List<Expression<Func<T, object>>>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            var query = _dbSet.AsQueryable();

            // Áp dụng Specification nếu có
            query = query.ApplySpecification(spec);

            // Áp dụng Include nếu có
            query = query.ApplyIncludes(includes);

            // Áp dụng OrderBy nếu có
            query = query.ApplyOrder(orderBy);

            // Áp dụng AsNoTracking nếu không cần theo dõi thay đổi
            query = query.AsNoTracking();

            return query.FirstOrDefaultAsync();
        }

        public async Task<T?> GetByEmailAsync(string email)
        {
            return await _dbSet.FindAsync(email);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
           return await _dbSet.FindAsync(id);
        }

        public Task<List<T>> ToListAsync(Specification<T>? spec = null, List<Expression<Func<T, object>>>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? page = null, int? size = null)
        {
            return _dbSet.AsNoTracking()
                 .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .ApplyPaging(page, size)
                .ToListAsync();
        }

        public Task<List<TDto>> ToListAsync<TDto>(Specification<T>? spec = null, List<Expression<Func<T, object>>>? includes = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int? page = null, int? size = null)
        {
            return _dbSet.AsNoTracking()
                .ApplySpecification(spec)
                .ApplyOrder(orderBy)
                .ApplyPaging(page, size)
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }

    internal static class RepositoryExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int? page, int? size)
        {
            if (page != null && size != null)
            {
                return query.Skip((page.Value - 1) * size.Value).Take(size.Value);
            }
            return query;
        }

        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, Specification<T>? spec)
        {
            if (spec != null)
            {
                query = query.ApplyFilter(spec.ToExpression());
            }
            return query;
        }

        public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>>? predicate)
        {
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        public static IQueryable<T> ApplyOrder<T>(this IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy)
        {
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query;
        }

        public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, List<Expression<Func<T, object>>>? includes) where T : class
        {
            if (includes != null)
            {
                // Áp dụng tất cả các Include vào truy vấn
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query;
        }
    }
}
