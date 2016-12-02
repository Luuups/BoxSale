using System.Web;
using System.Web.Optimization;

namespace MyBoxSale
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/fw/jquery-{version}.js",
                        "~/Scripts/fw/jquery-ui-{version}.js",
                        "~/Scripts/fw/bootstrap.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css",    
                "~/Content/site.css",
                "~/Content/assets/animate.css"));

            bundles.Add(new StyleBundle("~/bundles/Empresa").Include(
                "~/Scripts/Aplicacion/Empresa.js"));
        }
    }
}