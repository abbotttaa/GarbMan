using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GarbMan.Startup))]
namespace GarbMan
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
