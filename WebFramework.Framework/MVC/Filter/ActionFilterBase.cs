using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using WebFramework.Framework.Services.ApiSecurity;
using WebFramework.Framework.Types.Models;
using WebFramework.Infrastructure.Ulitily.Extensions;

namespace WebFramework.Framework.MVC.Filter
{
    public class ActionFilterBase: System.Web.Mvc.ActionFilterAttribute
    {
        protected WfRequestContext MvcContext
        {
            get { return Context.Get<WfRequestContext>("RequestContext"); }
        }

        protected T GetService<T>()
        {
            var scope = AutofacDependencyResolver.Current.RequestLifetimeScope;
            return scope.Resolve<T>();
        }

        protected static T GetServiceNamed<T>(string name)
        {
            var scope = AutofacDependencyResolver.Current.RequestLifetimeScope;
            return scope.ResolveNamed<T>(name);
        }

        protected HttpContext Context
        {
            get { return HttpContext.Current; }
        }

        public IApiSecurityService ApiSecurityService()
        {
            return GetService<IApiSecurityService>();
        }
    }
}
