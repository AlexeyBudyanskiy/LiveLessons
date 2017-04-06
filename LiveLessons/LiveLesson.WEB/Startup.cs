using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LiveLesson.WEB.Startup))]

namespace LiveLesson.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
