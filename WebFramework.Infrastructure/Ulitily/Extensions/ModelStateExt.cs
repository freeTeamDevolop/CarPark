using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.Infrastructure.Ulitily.Extensions
{
    public static class ModelStateExt
    {
        public static string GetError(this System.Web.Mvc.ModelStateDictionary state)
        {
            string error = string.Empty;
            foreach (var ms in state.Values)
            {
                foreach (var e in ms.Errors)
                {
                    if (!string.IsNullOrWhiteSpace(e.ErrorMessage))
                        error = string.Concat(error, e.ErrorMessage, ";");
                    else if (e.Exception != null)
                    {
                        var exp = e.Exception;
                        if (exp is JsonReaderException)
                        {
                            error = string.Concat(error, "wrong json format!!");
                        }
                        else
                        {
                            error = string.Concat(error, e.Exception.Message, ";");
                        }
                    }
                }
            }
            return error;
        }

        public static string GetError(this System.Web.Http.ModelBinding.ModelStateDictionary state)
        {
            string error = string.Empty;
            foreach (var ms in state.Values)
            {
                foreach (var e in ms.Errors)
                {
                    if (!string.IsNullOrWhiteSpace(e.ErrorMessage))
                        error = string.Concat(error, e.ErrorMessage, ";");
                    else if (e.Exception != null)
                    {
                        var exp = e.Exception;
                        if (exp is JsonReaderException)
                        {
                            error = string.Concat(error, "wrong json format!!");
                        }
                        else
                        {
                            error = string.Concat(error, e.Exception.Message, ";");
                        }
                    }
                }
            }
            return error;
        }
    }
}
