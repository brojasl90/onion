using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                      "~/Scripts/jquery-ui.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryajax").Include(
                        "~/Scripts/jquery.unobtrusive*"));

            // SweetAlert
            bundles.Add(new ScriptBundle("~/bundles/sweetalert").Include("~/Scripts/sweetalert.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap_almacen.min.css", "~/Content/jquery-ui.css", "~/Content/sweetalert.css"));
            // "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(//"~/Content/bootstrap.css", //"~/Content/bootstrap-materia.min.css",
            //    "~/Content/jquery-ui.css","~/Content/sweetalert.css"));

            bundles.Add(new StyleBundle("~/bundles/chartjs").Include(
                "~/Scripts/chartjs/chartjs.min.js"));
        }
    }
}
