using System.Web;
using System.Web.Optimization;

namespace UI
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/profile").Include(
                       "~/Scripts/angular.js", 
                       "~/Scripts/angular-route.js",
                       "~/Scripts/Application/WunderlistApp.js",
                       "~/Scripts/Application/Service.js",
                       "~/Scripts/Application/Controller.js"));

                 bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/normalize.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/UserProfile").Include(
                      "~/Content/normalize.css",
                      "~/Content/Profile.css"));
        }
    }
}
