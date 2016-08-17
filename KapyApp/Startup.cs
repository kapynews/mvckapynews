using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KapyApp.Startup))]
namespace KapyApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
