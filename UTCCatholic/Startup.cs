using Microsoft.Owin;
using Owin;
using UTCCatholic;

[assembly: OwinStartupAttribute(typeof(UTCCatholic.Startup))]
namespace UTCCatholic
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app)
		{
			app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
