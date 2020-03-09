using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using WebFramework.Framework.Api.Filters;
using WebFramework.WebApiControllers.Services.Users;

namespace WebFramework.WebApiControllers.Extension.Filters
{
    public class ApiControllerFilter: ActionFilterBase
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var users = GetService<IUsersService>(actionContext);

            if (!users.isValidUser())
            {
                SetFailedResult(actionContext, "非有效用户");
            }
        }
    }
}
