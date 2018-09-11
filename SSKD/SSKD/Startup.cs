using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSKD.Startup))]
namespace SSKD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
