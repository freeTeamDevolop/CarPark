using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Framework.Api.Controllers;
using WebFramework.Framework.Api.Filters;
using WebFramework.WebApiControllers.Extension.Filters;

namespace WebFramework.WebApiControllers.Extension
{
    [PermissionFilter("Web")]
    //[ApiControllerFilter]
    public class WebAuthorityApiController:ApiCoreController
    {

    }
}
