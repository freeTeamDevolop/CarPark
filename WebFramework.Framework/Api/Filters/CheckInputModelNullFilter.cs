using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace WebFramework.Framework.Api.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckInputModelNullFilter : ActionFilterAttribute
    {
        public class NonNullParameterWhichIsNull
        {
            public string ParameterName { get; set; }
            public Type ParameterType { get; set; }
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
            var parametersWithNull = GetNonNullParametersWhichAreNull(context);

            // Find incoming arguments which are null and part of our parameter list.
            var argumentsWithNull = context.ActionArguments.Where(
                x => parametersWithNull.Any(
                    y => y.ParameterName == x.Key) && x.Value == null)
                    .ToList();

            // For each null-argument, create a default instance.
            foreach (var argument in argumentsWithNull)
            {
                context.ModelState.AddModelError(argument.Key, "Request model can't be empty!");
                //var model = Activator.CreateInstance(parametersWithNull.First(x => x.ParameterName == argument.Key).ParameterType);
                //context.ActionArguments[argument.Key] = model;
                //TryValidateModel(context, model);
            }
        }

        /// <summary>
        /// Finds all action parameters which are null (non-nullable) and not
        /// simple types.
        /// </summary>
        protected IEnumerable<NonNullParameterWhichIsNull> GetNonNullParametersWhichAreNull(HttpActionContext context)
        {
            var parameters = context.ActionDescriptor.GetParameters()
                .Where(p => !p.IsOptional && p.DefaultValue == null &&
                    !p.ParameterType.IsValueType &&
                        p.ParameterType != typeof(string))
                 .Select(p => new NonNullParameterWhichIsNull
                 {
                     ParameterName = p.ParameterName,
                     ParameterType = p.ParameterType
                 })
                 .ToList();

            return parameters;
        }

        protected virtual bool TryValidateModel(HttpActionContext context, object model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }

            var metadataProvider = context.RequestContext.Configuration.Services.GetService(typeof(ModelMetadataProvider)) as ModelMetadataProvider;
            var validatorProviders = context.RequestContext.Configuration.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>();
            var metadata = metadataProvider.GetMetadataForType(() => model, model.GetType());

            context.ModelState.Clear();
            var modelValidators = metadata.GetValidators(validatorProviders);
            foreach (var validationResult in modelValidators.SelectMany(v => v.Validate(metadata, null)))
            {
                context.ModelState.AddModelError(validationResult.MemberName, validationResult.Message);
            }

            return context.ModelState.IsValid;
        }
    }
}
