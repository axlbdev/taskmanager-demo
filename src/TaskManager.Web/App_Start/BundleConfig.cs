using System.Web;
using System.Web.Optimization;

namespace TaskManager.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.signalR-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-resource.js",
                        "~/Scripts/angular-signalr-hub.js",
                        "~/Scripts/angular-ui-router.js",
                        "~/Scripts/angular-mocks.js",
                        "~/Scripts/ngStorage.js"
                        ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                    "~/Application/auth/module.js",
                    "~/Application/auth/services/securityService.js",
                    "~/Application/common/module.js",
                    "~/Application/sockets/module.js",
                    "~/Application/sockets/services/socketsService.js",
                    "~/Application/taskmanager/module.js",
                    "~/Application/taskmanager/routes/root.js",
                    "~/Application/taskmanager/routes/tasks.js",
                    "~/Application/taskmanager/routes/settings.js",
                    "~/Application/taskmanager/views/rootController.js",
                    "~/Application/taskmanager/views/index/indexController.js",
                    "~/Application/taskmanager/views/tasks/tasksController.js",
                    "~/Application/taskmanager/views/settings/settingsController.js",
                    "~/Application/taskmanager/components/tm-task-edit/tm-task-edit.js",
                    "~/Application/taskmanager/components/tm-login-box/tm-login-box.js"
                ));
        }
    }
}
