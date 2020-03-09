using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Data.Infrastructure;
using WebFramework.Data.Models;
using WebFramework.Framework.Services.Common;
using WebFramework.Framework.Types.Models;

namespace WebFramework.Framework.Services.ApiSecurity
{
    public class ApiSecurityService
        : DbServiceBase<WfAppKey>, IApiSecurityService
    {
        
        public ApiSecurityService(
            IRepository<WfAppKey> repo
            )
            : base(repo)
        {
            
        }


        #region IApiSecurityService 成员

        public ApiSecurityInfo Get(string appkey)
        {
            var kr = Repo.Table.FirstOrDefault(a => a.AppKey == appkey);
            if (kr == null)
                return null;
            return new ApiSecurityInfo
            {
                AppSecret = kr.AppSecret,
            };
        }
        #endregion
    }
}
