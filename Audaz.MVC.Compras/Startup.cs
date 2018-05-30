using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Audaz.MVC.Compras.Startup))]
namespace Audaz.MVC.Compras
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
