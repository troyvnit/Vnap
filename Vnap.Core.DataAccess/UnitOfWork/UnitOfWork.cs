using System;
using Microsoft.EntityFrameworkCore;

namespace Vnap.Core.DataAccess.UnitOfWork
{
    public class UnitOfWork : UnitOfWorkBase<DbContext>, IUnitOfWork
    {
        public UnitOfWork(DbContext context, IServiceProvider provider) : base(context, provider)
        { }
    }
}
