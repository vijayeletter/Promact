using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Promact.Startup))]
namespace Promact
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
