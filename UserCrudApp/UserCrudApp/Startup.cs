using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserCrudApp.Startup))]
namespace UserCrudApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
