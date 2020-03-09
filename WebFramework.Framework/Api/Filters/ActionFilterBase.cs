using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebFramework.Framework.Data;
using WebFramework.Framework.Services.ApiSecurity;
using WebFramework.Framework.Types.Models;
using WebFramework.Infrastructure.Ulitily.Extensions;

namespace WebFramework.Framework.Api.Filters
{
    public class ActionFilterBase: ActionFilterAttribute
    {

        protected WfRequestContext ApiContext()
        {
            return Context().Get<WfRequestContext>("RequestContext");
        }

        protected T GetService<T>(HttpActionContext filterContext, string key)
        {
            var dependencyScope = filterContext.Request.RequestLifetimeScope();
            return dependencyScope.ResolveKeyed<T>(key);
        }
        protected T GetService<T>(HttpActionContext filterContext)
        {
            var dependencyScope = filterContext.Request.RequestLifetimeScope();
            return dependencyScope.Resolve<T>();
        }

        public IApiSecurityService ApiSecurityService(HttpActionContext filterContext)
        {
            return GetService<IApiSecurityService>(filterContext);
        }

        protected void SetFailedResult(HttpActionContext filterContext, string reason)
        {
            //var localizationService = GetService<ILocalizationService>(filterContext);
            var result = RequestResult<string>.Get();
            result.error = reason;
            //result.error = localizationService.GetResource(LocalizationString.CommonAccessDenied);
            filterContext.Response = new HttpResponseMessage { Content = new StringContent(result.ToJson()) };
            ApiContext().RequestResult = reason;
        }


        protected HttpContext Context()
        {
            return HttpContext.Current;
        }
    }
}
