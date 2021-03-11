using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OlifransShopping.Startup))]
namespace OlifransShopping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
