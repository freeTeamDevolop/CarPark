using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using WebFramework.Framework.AspNet.Filters;

namespace WebFramework.Framework.Api.Filters
{
    public class PermissionFilter: ActionFilterBase
    {
        private readonly string _typeKey;
        public PermissionFilter(string typeKey)
        {
            _typeKey = typeKey;
        }
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            var ad = filterContext.ActionDescriptor;
            var reason = "success";
            var nonAuth = false;
            var nonAuthorityAttrs = ad.GetCustomAttributes<NonAuthority>(true);
            if (nonAuthorityAttrs.Any())
            {
                nonAuth = true;
            }
            var access = true;
            var header = filterContext.Request.Headers;
            if (header.Authorization == null)
            {
                access = false;
                reason = "no authorization";
            }
            else
            {
                string auth = string.Empty;
                try
                {
                    auth = header.Authorization.Parameter;
                }
                catch
                {

                }
            }
            if (!access && !nonAuth)
            {
                SetFailedResult(filterContext, reason);
            }
        }
    }
}
