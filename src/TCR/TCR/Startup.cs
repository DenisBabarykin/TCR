using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TCR.Startup))]
namespace TCR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
