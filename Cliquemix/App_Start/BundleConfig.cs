using System.Web;
using System.Web.Optimization;

namespace Cliquemix
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            */

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
          /*   bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));  
          */


  /*********************** JS bundles *******************************************/

            bundles.Add(new ScriptBundle("~/bundles/js-default").Include(
                       "~/Scripts/plugins/bootstrap/bootstrap.min.js",
                       "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js",
                       "~/Scripts/plugins/popupoverlay/jquery.popupoverlay.js",
                       "~/Scripts/plugins/popupoverlay/defaults.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/js-pace").Include(
                       "~/Scripts/plugins/pace/pace.js"    
                        ));

             bundles.Add(new ScriptBundle("~/bundles/js-others").Include(
                       "~/Scripts/plugins/popupoverlay/logout.js",
                        "~/Scripts/plugins/hisrc/hisrc.js",
                        "~/Scripts/plugins/messenger/messenger.min.js",
                        "~/Scripts/plugins/messenger/messenger-theme-flat.js",
                        "~/Scripts/plugins/daterangepicker/moment.js",
                        "~/Scripts/plugins/daterangepicker/daterangepicker.js",
                        "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                        "~/Scripts/plugins/morris/morris.js",
                        "~/Scripts/plugins/flot/jquery.flot.js",
                       "~/Scripts/plugins/flot/jquery.flot.resize.js",
                        "~/Scripts/plugins/sparkline/jquery.sparkline.min.js",
                        "~/Scripts/plugins/moment/moment.min.js",
                        "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                        "~/Scripts/plugins/jvectormap/maps/jquery-jvectormap-world-mill-en.js",
                        "~/Scripts/demo/map-demo-data.js",
                        "~/Scripts/plugins/easypiechart/jquery.easypiechart.min.js",
                        "~/Scripts/plugins/datatables/jquery.dataTables.js",
                        "~/Scripts/plugins/datatables/datatables-bs3.js",
                        "~/Scripts/flex.js"
                        ));


            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
