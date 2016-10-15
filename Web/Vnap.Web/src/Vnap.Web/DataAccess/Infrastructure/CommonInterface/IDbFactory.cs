using System;
using Vnap.Web.Data;

namespace Vnap.Web.DataAccess
{
    public interface IDbFactory : IDisposable
    {
        ApplicationDbContext Init();
    }
}
