using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Follian.WebUI.Startup))]
namespace Follian.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
