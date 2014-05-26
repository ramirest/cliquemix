using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cliquemix.Startup))]
namespace cliquemix
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
