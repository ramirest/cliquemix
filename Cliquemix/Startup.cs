using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cliquemix.Startup))]
namespace Cliquemix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
