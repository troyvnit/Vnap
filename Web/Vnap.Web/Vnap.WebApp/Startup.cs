using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;
using Vnap.WebApp.Hubs;
using Vnap.Web.DataAccess.Repository;
using Vnap.Web.DataAccess;

[assembly: OwinStartup(typeof(Vnap.WebApp.Startup))]

namespace Vnap.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(typeof(NotificationHub), () => new NotificationHub(new ConversationRepository(new DbFactory())));
            var config = new HubConfiguration();
            config.EnableJSONP = true;
            app.MapSignalR(config);
            ConfigureAuth(app);
        }
    }
}
