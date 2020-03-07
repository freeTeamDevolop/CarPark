using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebFramework.App_Start;
using WebFramework.WebApiControllers.Configuration;

namespace WebFramework
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //×¢²áAutofac;£¨1£©
            //AutofacConfig_WebApi.Regist();


            //×¢²áAutofac£»£¨2£©
            //var builder = new ContainerBuilder();

            //AutofacBuilder.SetupResolveRules(builder);
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());


            //builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);
            //builder.RegisterWebApiModelBinderProvider();

            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces().AsSelf();

            //var container = builder.Build();

            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            //GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //×¢²áAutofac(3)
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.Load("WebFramework.WebApiControllers"));

            AutofacBuilder.SetupResolveRules(builder);

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);


        }
    }
}
