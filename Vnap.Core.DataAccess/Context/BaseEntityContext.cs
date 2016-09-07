using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vnap.Core.DataAccess.Entity;

namespace Vnap.Core.DataAccess.Context
{
    interface IEntityContext { }

    public class BaseEntityContext<TContext> : IdentityDbContext<ApplicationUser>, IEntityContext where TContext : IdentityDbContext<ApplicationUser>
    {
        public BaseEntityContext(DbContextOptions<TContext> options) : base(options)
        {
        }
    }
}
