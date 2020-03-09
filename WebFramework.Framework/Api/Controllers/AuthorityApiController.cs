using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Framework.Api.Filters;

namespace WebFramework.Framework.Api.Controllers
{
    [PermissionFilter("App")]
    public abstract class AuthorityApiController: ApiCoreController
    {

    }
}
