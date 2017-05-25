using System;
using Vnap.WebApp.Models;

namespace Vnap.Web.DataAccess
{
    public interface IDbFactory : IDisposable
    {
        ApplicationDbContext Init();
    }
}
