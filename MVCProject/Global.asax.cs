using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MVCProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var db = new ApplicationDbContext();
            var initialCount = db.Counters.FirstOrDefault(x => x.CounterId == 1);
            Application["Totaluser"] = initialCount.CounterNumber;
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "MVCProject.Controllers" }
            );
        }
        protected void Session_Start()
        {
            var db = new ApplicationDbContext();
            var count = (int)Application["Totaluser"] + 1;
            Application.Lock();
            var result = db.Counters.FirstOrDefault(x => x.CounterId == 1);
            result.CounterNumber = count;
            db.SaveChanges();
            Application["Totaluser"] = count;
            Application.UnLock();
        }
    }
}
