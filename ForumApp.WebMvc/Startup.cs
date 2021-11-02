using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ForumApp.WebMvc.Startup))]
namespace ForumApp.WebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
