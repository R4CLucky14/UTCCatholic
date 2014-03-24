using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace UTCCatholic
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
			app.UseCookieAuthentication( new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString( "/Account/Login" )
			} );
			app.UseExternalSignInCookie( DefaultAuthenticationTypes.ExternalCookie );

            // Uncomment the following lines to enable logging in with third party login providers
			app.UseMicrosoftAccountAuthentication(
				clientId: "000000004810F526",
				clientSecret: "f8LRRVF20XHwK6N58e4I9r99SEC7Ao0S" );

			app.UseTwitterAuthentication(
			   consumerKey: "PqRSqRIrWw8EJty1Gat0ZA",
			   consumerSecret: "gsElizO6qYGPA1RMApWdp1BpsmfvRYXXBWmd0hs8" );

			app.UseFacebookAuthentication(
			   appId: "449783478408068",
			   appSecret: "a058d3068f6b29bc0149e5987b9e823a" );

            app.UseGoogleAuthentication();
        }
    }
}