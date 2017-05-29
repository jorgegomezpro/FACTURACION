using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TESTINGFACT.Startup))]
namespace TESTINGFACT
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
