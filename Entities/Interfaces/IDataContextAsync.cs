using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces
{
    public interface IDataContextAsync : IDataContext
    {
        Task<int> SaveChangesAsync();

        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
    }
}
