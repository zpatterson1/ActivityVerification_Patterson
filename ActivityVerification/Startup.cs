using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ActivityVerification.Startup))]
namespace ActivityVerification
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
