using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WebFramework.Framework.Data;

namespace WebFramework.Framework.Api.Controllers
{
    public abstract class ApiCoreController : ApiController
    {
        protected bool F<T>(RequestResult<T> result)
        {
            if (ModelState.IsValid)
                return true;
            result.GetModelStateError(ModelState);
            return false;
        }

        protected RequestResult<T> F<T>(Func<RequestResult<T>> func)
        {
            var result = RequestResult<T>.Get();
            if (ModelState.IsValid)
            {
                result = func();
            }
            else
                result.GetModelStateError(ModelState);
            return result;
        }

        protected RequestResult<T> F<T>(Func<T> func)
        {
            var result = RequestResult<T>.Get();
            if (ModelState.IsValid)
                result.result = func();
            else
                result.GetModelStateError(ModelState);
            return result;
        }

        protected RequestResult<bool> F(Action func)
        {
            var result = RequestResult<bool>.Get();
            if (ModelState.IsValid)
            {
                func();
                result.result = true;
            }
            else
                result.GetModelStateError(ModelState);
            return result;
        }

        protected async Task<RequestResult<T>> F<T>(Func<Task<T>> func)
        {
            var result = RequestResult<T>.Get();
            if (ModelState.IsValid)
                result.result = await func();
            else
                result.GetModelStateError(ModelState);
            return result;
        }

        protected async Task<RequestResult<T>> F<T>(Func<Task<RequestResult<T>>> func)
        {
            var result = RequestResult<T>.Get();
            if (ModelState.IsValid)
                result = await func();
            else
                result.GetModelStateError(ModelState);
            return result;
        }
    }
}
