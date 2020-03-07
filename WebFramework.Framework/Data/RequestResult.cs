using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFramework.Infrastructure.Ulitily.Extensions;

namespace WebFramework.Framework.Data
{
    public class RequestResult<T>
    {
        public bool success { get; set; }

        private string _error;
        [JsonIgnore]
        public string message { get; set; }

        /// <summary>
        ///     Success为false时，该属性报告遇到的错误
        /// </summary>
        public string error
        {
            get { return _error; }
            set
            {
                result = default(T);
                success = false;
                _error = value;
            }
        }

        /// <summary>
        ///     该属性包含返回的结果
        /// </summary>
        public T result { get; set; }

        public PerformanceAnalysisData performance { get; set; }

        public RequestResult()
        {
        }

        public static RequestResult<T> Get()
        {
            return new RequestResult<T>() { success = true, performance = new PerformanceAnalysisData() };
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void GetModelStateError(System.Web.Mvc.ModelStateDictionary modelState)
        {
            error = modelState.GetError();
        }
        public void GetModelStateError(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            error = modelState.GetError();
        }

        public void SetModelStateError(System.Web.Mvc.ModelStateDictionary modelState)
        {
            if (!string.IsNullOrEmpty(error))
            {
                modelState.AddModelError("error", error);
            }
        }

        public void SetModelStateError(System.Web.Http.ModelBinding.ModelStateDictionary modelState)
        {
            if (!string.IsNullOrEmpty(error))
            {
                modelState.AddModelError("error", error);
            }
        }
    }
}
