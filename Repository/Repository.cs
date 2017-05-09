using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        private readonly IDataContextAsync _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IDataContextAsync context)
        {
            _context = context;

            var dbContext = context as DbContext;

            if (dbContext != null)
                _dbSet = dbContext.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Queryable() => _dbSet;

        public void Insert(TEntity entity)
        {
            entity.ObjectState = ObjectState.Added;
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            entity.ObjectState = ObjectState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public int Save() => _context.SaveChanges();
    }
}
