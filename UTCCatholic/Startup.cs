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

			ConfigureAuth( app );
			//var hubConfiguration = new HubConfiguration();
			//hubConfiguration.EnableDetailedErrors = true;
			//app.MapSignalR( hubConfiguration );
			app.MapSignalR();
        }
    }
}
