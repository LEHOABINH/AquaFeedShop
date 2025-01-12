using AquaFeedShop.infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaFeedShop.core.Interfaces;

namespace AquaFeedShop.infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AquaFeedShopContext _dbContext;
        private IDbContextTransaction _transaction;
        public IUserRepository Users { get; }
        public IProductRepository Products { get; }

        public UnitOfWork(AquaFeedShopContext dbContext,
                          IUserRepository userRepository,
                          IProductRepository productRepository
                           )
        {
            _dbContext = dbContext;
            Users = userRepository;
            Products = productRepository;
        }

        public void BeginTransaction()
        {
            _transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");
            _transaction.Commit();
            _transaction.Dispose();
        }

        public void Rollback()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Transaction has not been started.");
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
