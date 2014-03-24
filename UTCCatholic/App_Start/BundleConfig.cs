using System.Web;
using System.Web.Optimization;

namespace UTCCatholic
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.Add( new ScriptBundle( "~/bundles/jquery" ).Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery.unobtrusive*",
						"~/Scripts/jquery.validate*",
						"~/Scripts/jquery-ui-*",
						"~/Scripts/jquery.tmpl*",
						"~/Scripts/modernizr-*",
						"~/Scripts/metro-*",
						"~/Scripts/jquery-ui-{version}.js",
						"~/Scripts/jquery.signalR-2.0.0.js",
						"~/Scripts/app.*",
						"~/Scripts/admin.*",
						"~/Scripts/knockout-3.0.0.js" ) );

			bundles.Add( new StyleBundle( "~/Content/css" ).Include(
					  "~/Content/metro-bootstrap*",
					  "~/Content/bootstrap.css",
					  "~/Content/site.css" ) );
        }
    }
}
