using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Beerswap.Startup))]
namespace Beerswap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
