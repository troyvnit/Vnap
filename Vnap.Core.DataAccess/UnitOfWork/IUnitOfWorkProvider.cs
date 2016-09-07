using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vnap.Core.DataAccess.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork CreateUnitOfWork(bool trackChanges = true, bool enableLogging = false);
    }
}
