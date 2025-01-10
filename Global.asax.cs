using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using HemenBiletProje.Services;
using HemenBiletProje.Repositories;
using Ninject.Web.Mvc;
using HemenBiletProje.Entities;
using HemenBiletProje.Models;
using System.Data.Entity;

namespace HemenBiletProje
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IKernel Kernel { get; private set; }

        protected void Application_Start()
        {
            Kernel = new StandardKernel();
            RegisterServices(Kernel);

            DependencyResolver.SetResolver(new NinjectDependencyResolver(Kernel));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterServices(IKernel kernel)
        { 
            kernel.Bind<FlightContext>().ToSelf().InRequestScope();
            kernel.Bind<ISeatRepository>().To<SeatRepository>().InRequestScope();
            kernel.Bind<UserRepository>().ToSelf().InRequestScope();
            kernel.Bind<UserService>().ToSelf().InRequestScope();
            
            
        }
    }
}
