using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(beerswap.Startup))]
namespace beerswap
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
