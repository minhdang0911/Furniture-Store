using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BaiTapLon.Startup))]
namespace BaiTapLon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
