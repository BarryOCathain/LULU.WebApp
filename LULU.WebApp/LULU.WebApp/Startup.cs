using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LULU.WebApp.Startup))]
namespace LULU.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
