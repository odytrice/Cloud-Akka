using CloudAkka.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CloudAkka.Web
{
    public class MvcApplication : HttpApplication
    { 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AkkaSystem.Create();
        }

        protected void Application_End()
        {
            AkkaSystem.Shutdown();
        }
    }
}
