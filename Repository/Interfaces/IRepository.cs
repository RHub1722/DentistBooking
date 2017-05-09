using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Queryable();

        void Insert(TEntity entity);

        void Delete(TEntity entity);

        int Save();
    }
}
