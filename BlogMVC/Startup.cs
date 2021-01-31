using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogMVC.Startup))]
namespace BlogMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
