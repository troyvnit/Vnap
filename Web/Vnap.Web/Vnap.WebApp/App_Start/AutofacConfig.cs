using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Vnap.Web.DataAccess;
using Vnap.Web.DataAccess.Entity;
using Vnap.Web.DataAccess.Repository;
using Vnap.WebApp.Models;

namespace Vnap.WebApp
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            var db = new ApplicationDbContext();
            builder.Register<ApplicationDbContext>(c => db);
            builder.Register<UserStore<ApplicationUser>>(c => new UserStore<ApplicationUser>(db)).AsImplementedInterfaces();
            builder.Register<IdentityFactoryOptions<ApplicationUserManager>>(c => new IdentityFactoryOptions<ApplicationUserManager>()
            {
                DataProtectionProvider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("Vnap")
            });
            builder.RegisterType<ApplicationUserManager>();

            // Repositories
            builder.RegisterAssemblyTypes(typeof(PlantRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}