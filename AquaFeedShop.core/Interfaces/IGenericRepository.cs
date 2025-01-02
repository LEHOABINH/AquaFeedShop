﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AquaFeedShop.core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);

        Task<TEntity?> GetByIDAsync(object id);

        Task InsertAsync(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        void Update(TEntity entityToUpdate);

        Task<int> CountAsync();

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
    }
}