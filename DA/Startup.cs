using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DA.Startup))]
namespace DA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
