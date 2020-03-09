using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Autofac.Integration.WebApi;
using System.Web;

namespace WebFramework.Infrastructure.Ulitily.Extensions
{
    public static class HttpRequestMessageExt
    {
        public static string GetClientIp(this HttpRequestMessage request)
        {
            //如果客户端使用了代理服务器，则利用HTTP_X_FORWARDED_FOR找到客户端IP地址
            string userHostAddress = string.Empty;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0].Trim();
            }
            //否则直接读取REMOTE_ADDR获取客户端IP地址
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            //前两者均失败，则利用Request.UserHostAddress属性获取IP地址，但此时无法确定该IP是客户端IP还是代理IP
            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.UserHostAddress;
            }
            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIP(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }


        public static string Lang(this HttpRequestMessage request)
        {
            var lang = request.Headers.AcceptLanguage.Select(v => v.Value).ToList();
            var c = lang.FirstOrDefault();
            return c;
        }

        public static async Task<string> GetBody(this HttpRequestMessage request)
        {
            //if (request.Content.IsMimeMultipartContent())
            //    return string.Empty;
            var reqStream = await request.Content.ReadAsStreamAsync();
            if (reqStream.CanSeek)
            {
                reqStream.Position = 0;
            }
            var body = await request.Content.ReadAsStringAsync();
            reqStream.Position = 0;
            return body;
        }

        public static ILifetimeScope RequestLifetimeScope(this HttpRequestMessage context)
        {
            var dependencyScope = context.GetDependencyScope();
            if (dependencyScope != null)
            {
                var requestLifetimeScope = dependencyScope.GetRequestLifetimeScope();
                return requestLifetimeScope;
            }
            return null;
        }
    }
}
