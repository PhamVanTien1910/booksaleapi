using BookSale.Application.Repositories;
using BookSale.Infrastructure.Migrations.Data;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private bool _disposed = false;
        private IDbContextTransaction? _transaction;
        public UnitOfWork(DataContext context) 
        { 
            _context = context;
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            if(_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction = null;
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _transaction?.Dispose();
                _context.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
