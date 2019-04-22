using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FullCalenderEvent.Startup))]
namespace FullCalenderEvent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
