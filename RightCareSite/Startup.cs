using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RightCareSite.Startup))]
namespace RightCareSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
