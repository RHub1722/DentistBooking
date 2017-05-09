using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Interfaces;
using Repository.Interfaces;

namespace Repository
{
    public class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        private readonly IList<TEntity> _fakeData;

        public FakeRepository(IList<TEntity> fakeData)
        {
            _fakeData = fakeData;
        }
        public IQueryable<TEntity> Queryable()
        {
            return _fakeData.AsQueryable();
        }

        public void Insert(TEntity entity)
        {
            var id = _fakeData.OrderByDescending(x => x.Id).FirstOrDefault()?.Id;
            if (id.HasValue)
                entity.Id = id.Value + 1;
            else
                entity.Id = 1;

            _fakeData.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _fakeData.Remove(entity);
        }

        public int Save()
        {
            return default(int);
        }
    }
}
