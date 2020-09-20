using System.Web;
using System.Web.Optimization;

namespace WebPanit
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));
            bundles.Add(new ScriptBundle("~/bundles/Slick/Script").Include(
                      "~/Scripts/Slick/slick.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/PagedList.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/adminCss.css",
                      "~/Content/chosen.css",
                      "~/Content/PagedList.css"
                      ));
            bundles.Add(new StyleBundle("~/Content/Slick/css").Include(
                      "~/Content/Slick/slick.css"));
            bundles.Add(new StyleBundle("~/Content/Font/css/css").Include(
                      "~/Content/Font/css/all.css"));
            bundles.Add(new StyleBundle("~/Content/CustomerCss/css").Include(
                      "~/Content/CustomerCss/ClientCss.min.css"));
            BundleTable.EnableOptimizations = true;
        }
    }
}
