using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebFramework.Infrastructure.Ulitily.Extensions
{
    public static class HttpContextExt
    {
        public static T Get<T>(this HttpContext context, string key)
        {
            if (context == null || context.Items[key] == null)
            {
                return default(T);
            }
            var t = default(T);
            try
            {
                t = (T)context.Items[key];
            }
            catch
            {
                // ignored
            }
            return t;
        }

        public static void Set(this HttpContext context, string key, object value)
        {
            if (context != null)
                context.Items[key] = value;
        }

        public static ILifetimeScope RequestLifetimeScope(this HttpContext context)
        {
            var requestMessage = context.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            var dependencyScope = requestMessage.GetDependencyScope();
            if (dependencyScope != null)
            {
                var requestLifetimeScope = dependencyScope.GetRequestLifetimeScope();
                return requestLifetimeScope;
            }
            return null;
        }
    }
}
