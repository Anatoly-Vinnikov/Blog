using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog2.Startup))]
namespace Blog2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
