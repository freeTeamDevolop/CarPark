using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Framework.Types.Models;

namespace WebFramework.Framework.Services.ApiSecurity
{
    public interface IApiSecurityService
    {
        ApiSecurityInfo Get(string appkey);
    }
}
