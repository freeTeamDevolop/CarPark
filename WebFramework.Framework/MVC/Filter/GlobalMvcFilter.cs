using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Framework.Configuration;
using WebFramework.Framework.Types.Models;
using WebFramework.Infrastructure.Ulitily.Extensions;

namespace WebFramework.Framework.MVC.Filter
{
    public class GlobalMvcFilter:ActionFilterBase
    {
        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            var requestContext = new WfRequestContext { RequestResult = "ok" };
            Context.Set("RequestContext", requestContext);
            var context = filterContext.HttpContext;
            var request = context.Request;
            var ip = request.GetClientIp();
            requestContext.Context = Guid.NewGuid().ToString();
            requestContext.CallStart = DateTime.Now;
            requestContext.ClientIp = ip;
            var actionDescriptor = filterContext.ActionDescriptor;
            requestContext.RequestController = actionDescriptor.ControllerDescriptor.ControllerName;
            requestContext.RequestAction = actionDescriptor.ActionName;

            requestContext.ClientTimeZone = 8;
            requestContext.ClientVer = "1.0.0.0";
            requestContext.UserAgent = request.UserAgent;
            requestContext.AppKey = "6be59xofc78ox1e4oofb4fcyy14733140";
            requestContext.ApiRole = "framework";
            requestContext.Lang = request.Lang();
            if (!CultureConfig.ValidCultures.Contains(requestContext.Lang))
            {
                requestContext.Lang = CultureConfig.ValidCultures[0];
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
