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
						"~/Scripts/jquery.mousewheel*",
						"~/Scripts/modernizr-*",
						"~/Scripts/metroui/accordion.js",
						"~/Scripts/metroui/buttonset.js",
						"~/Scripts/metroui/calendar.js",
						"~/Scripts/metroui/carousel.js",
						"~/Scripts/metroui/dialog.js",
						"~/Scripts/metroui/dropdown.js",
						"~/Scripts/metroui/input-control.js",
						"~/Scripts/metroui/pagecontrol.js",
						"~/Scripts/metroui/rating.js",
						"~/Scripts/metroui/slider.js",
						"~/Scripts/metroui/start-menu.js",
						"~/Scripts/metroui/tile=drag.js",
						"~/Scripts/metroui/tile=slider.js",
						"~/Scripts/jquery-ui-{version}.js",
						"~/Scripts/jquery.signalR-2.0.0.js",
						"~/Scripts/knockout-3.0.0.js" ) );

			bundles.Add( new StyleBundle( "~/Content/css" ).Include(
					  "~/Content/metro-bootstrap*",
					  "~/Content/site.css" ) );

			bundles.Add( new StyleBundle( "~/Content/themes/base/css" ).Include(
						"~/Content/themes/base/jquery.ui.core.css",
						"~/Content/themes/base/jquery.ui.resizable.css",
						"~/Content/themes/base/jquery.ui.selectable.css",
						"~/Content/themes/base/jquery.ui.accordion.css",
						"~/Content/themes/base/jquery.ui.autocomplete.css",
						"~/Content/themes/base/jquery.ui.button.css",
						"~/Content/themes/base/jquery.ui.dialog.css",
						"~/Content/themes/base/jquery.ui.slider.css",
						"~/Content/themes/base/jquery.ui.tabs.css",
						"~/Content/themes/base/jquery.ui.datepicker.css",
						"~/Content/themes/base/jquery.ui.progressbar.css",
						"~/Content/themes/base/jquery.ui.theme.css" ) );

        }
    }
}
