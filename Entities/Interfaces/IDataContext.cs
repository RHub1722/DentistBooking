using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Interfaces
{
    public interface IDataContext: IDisposable
    {
        int SaveChanges();
    }
}
