using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PSA_Prototype_1.Startup))]
namespace PSA_Prototype_1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
