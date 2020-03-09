using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using WebFramework.Framework.AspNet.Filters;
using WebFramework.Framework.Data;
using WebFramework.Framework.Types.Models;
using WebFramework.Infrastructure.Ulitily.Extensions;
using WebFramework.Infrastructure.Ulitily.Security;

namespace WebFramework.Framework.Api.Filters
{
    public class SecurityFilter:ActionFilterBase
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext,
            System.Threading.CancellationToken cancellationToken)
        {
            var reason = "success;";
            var requestContext = new WfRequestContext { RequestResult = reason };
            requestContext.CallStart = DateTime.Now;
            var actionDescriptor = actionContext.ActionDescriptor;
            var header = actionContext.Request.Headers;
            var result = RequestResult<string>.Get();
            var ip = actionContext.Request.GetClientIp();
            Context().Set("RequestContext", requestContext);
            requestContext.Body = await actionContext.Request.GetBody();
            requestContext.Context = Guid.NewGuid().ToString();
            requestContext.ClientIp = ip;
            requestContext.AppIp = ip;
            requestContext.Lang = actionContext.Request.Lang();
            requestContext.RequestController = actionDescriptor.ControllerDescriptor.ControllerName;
            requestContext.RequestAction = actionDescriptor.ActionName;

            var localOnlySuccess = true;
            //var localOnlyAttrs = actionDescriptor.GetCustomAttributes<LocalAccessAttribute>(true);
            //if (localOnlyAttrs.Count > 0)
            //{
            //    if (!requestContext.AppIp.IsPrivateIpAddress())
            //    {
            //        localOnlySuccess = false;
            //    }
            //}
            if (localOnlySuccess)
            {
                var nonSecurityAttr = actionDescriptor.GetCustomAttributes<NonSecurityFilter>(true);
                if (nonSecurityAttr.Count > 0)
                {
                    if (header.Contains("entry-domain"))
                    {
                        var domain = header.GetValues("entry-domain").FirstOrDefault();
                        if (domain != null)
                            requestContext.EntryDomain = domain;
                    }
                    await base.OnActionExecutingAsync(actionContext, cancellationToken);
                    return;
                }
                else
                {
                    if (header.Contains("api-appkey") && header.Contains("api-sign") && header.Contains("device") &&
                        header.Contains("client-timezone") && header.Contains("client-version") && header.Contains("client-ip") && header.Contains("entry-domain"))
                    {
                        var clientIp = header.GetValues("client-ip").FirstOrDefault();
                        if (clientIp != null)
                            requestContext.ClientIp = clientIp;
                        var domain = header.GetValues("entry-domain").FirstOrDefault();
                        if (domain != null)
                            requestContext.EntryDomain = domain;
                        int tempZone;
                        var clientTimeZone = header.GetValues("client-timezone").FirstOrDefault();
                        if (!int.TryParse(clientTimeZone, out tempZone))
                            tempZone = -100;
                        if (tempZone == -100)
                            tempZone = 8;
                        requestContext.ClientTimeZone = tempZone;

                        requestContext.ClientVer = header.GetValues("client-version").FirstOrDefault();

                        var appkey = header.GetValues("api-appkey").FirstOrDefault(); //appkey:timestamp
                        var sign = header.GetValues("api-sign").FirstOrDefault(); //hash(appsecret:timestamp)
                        var device = header.GetValues("device").FirstOrDefault();

                        if (!string.IsNullOrWhiteSpace(appkey) && !string.IsNullOrWhiteSpace(sign) &&
                            !string.IsNullOrWhiteSpace(device))
                        {
                            requestContext.UserAgent = device + "-- Client:" + requestContext.ClientVer;
                            requestContext.Terminal = device;
                            if (header.Contains("sdk-version"))
                            {
                                var sdkVersion = header.GetValues("sdk-version").FirstOrDefault();
                                if (!string.IsNullOrWhiteSpace(sdkVersion))
                                {
                                    requestContext.UserAgent = requestContext.UserAgent + "-- Sdk:" + sdkVersion;
                                }
                            }
                            var keyTimePair = appkey.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            if (keyTimePair.Length == 2)
                            {
                                long tempTime;
                                if (long.TryParse(keyTimePair[1], out tempTime))
                                {
                                    var replayAttactAttr =
                                        actionDescriptor.GetCustomAttributes<ReplayAttackFilter>(true);
                                    if (replayAttactAttr.Count > 0)
                                    {
                                        var serverNow = DateTime.UtcNow.ToUnixTimeStamp();
                                        if (tempTime < serverNow - 120 || tempTime > serverNow + 120)
                                        {
                                            reason = "bad clock:" + (tempTime - serverNow).ToString();
                                            goto end;
                                        }
                                    }
                                    var temp = ApiSecurityService(actionContext);
                                    var app = temp.Get(keyTimePair[0]);
                                    //temp = null;
                                    if (app != null)
                                    {
                                        requestContext.AppKey = keyTimePair[0];

                                        var roleAttrs = actionDescriptor.GetCustomAttributes<RoleFilter>(true).ToList();
                                        var roleAttrsOnController =
                                            actionContext.ControllerContext.ControllerDescriptor
                                                .GetCustomAttributes<RoleFilter>().ToList();
                                        roleAttrs.AddRange(roleAttrsOnController);
                                        //var keyRoleName = app.RoleName;
                                        if (roleAttrs.Count > 0)
                                        {
                                            var authedRoles = new List<string>();
                                            roleAttrs.ForEach(rf => authedRoles.AddRange(rf.Roles));
                                            //if (!authedRoles.Contains(keyRoleName))
                                            //{
                                            //    reason = "api access deined";
                                            //    goto end;
                                            //}
                                        }
                                        //requestContext.ApiRole = keyRoleName;

                                        var body = requestContext.Body;
                                        var newSignStr =
                                            string.Format("{0}:{1}:{2}", app.AppSecret, keyTimePair[1], body);
                                        var newServerSign = HashAlgorithm.Sha256(newSignStr);
                                        if (newServerSign.Equals(sign, StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            await base.OnActionExecutingAsync(actionContext, cancellationToken);
                                            return;
                                        }
                                        else
                                        {
                                            reason = "" + sign; //appsecret mismatch,
                                        }
                                    }
                                    else
                                        reason = "no app info or app disabled";
                                }
                                else
                                    reason = "wrong timestamp";
                            }
                            else
                                reason = "wrong appkey or sign format";
                        }
                        else
                            reason = "no appkey or sign";
                    }
                    else
                    {
                        //var logs = AppLogService(actionContext);
                        //var sb = new StringBuilder();
                        //foreach (var h in header)
                        //{
                        //    sb.AppendFormat("{0}:{1}&", h.Key, string.Join(",", h.Value));
                        //}
                        //logs.CustomLog(sb.ToString(), LogLevel.Debug);
                        reason = "no appkey or sign or device or other";
                    }
                }
            }
            else
            {
                reason = "local access only";
            }
        end:
            requestContext.RequestResult = reason;
            var endDate = DateTime.Now;
            var ts = (endDate - requestContext.CallStart).TotalMilliseconds;
            requestContext.RequestTimeCast = ts;
            //AddCallLog(actionContext);
            //var los = LocalizationService(actionContext);
            //result.error = los.GetResource(LocalizationString.CommonAccessDenied);
            //actionContext.Response = new HttpResponseMessage { Content = new StringContent(result.ToJson()) };
        }

        private WfRequestContext WfRequestContext
        {
            get { return HttpContext.Current.Get<WfRequestContext>("RequestContext"); }
        }
    }
}
