using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebFramework.Data.Infrastructure;
using WebFramework.Framework.Types.Models;
using WebFramework.Infrastructure.Ulitily.Extensions;

namespace WebFramework.Framework.Services.Common
{
    public abstract class ServiceBase
    {
        protected WfRequestContext WfRequestContext
        {
            get { return HttpContext.Current.Get<WfRequestContext>("RequestContext"); }
        }

        protected ILifetimeScope ApiContainer
        {
            get { return HttpContext.Current.RequestLifetimeScope(); }
        }
        protected T GetContext<T>(string key)
        {
            return HttpContext.Current.Get<T>(key);
        }

        protected DateTime Now
        {
            get { return DateTime.Now; }
        }

        protected long UnixNow(bool formatToTomorrow = false, int timeZone = 8)
        {
            return DateTime.UtcNow.ToUnixTimeStamp(formatToTomorrow, timeZone);
        }

        protected IRepository<T> ResolveContextLocator<T>(object target) where T : class
        {
            return ApiContainer.Resolve<IRepository<T>>(new NamedParameter("target", target));
        }
    }
}
