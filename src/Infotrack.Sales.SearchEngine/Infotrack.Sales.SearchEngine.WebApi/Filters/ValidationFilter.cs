using Infotrack.Sales.SearchEngine.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;


namespace Infotrack.Sales.SearchEngine.WebApi.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var messages = GetValidationErrorMessages(context);

                context.Result = new BadRequestObjectResult(
                    new ExceptionResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.BadRequest,
                        Description = messages
                    });
            }

        }

        private string GetValidationErrorMessages(ActionExecutingContext context)
        {
            var messages = context.ModelState.Values
               .Where(x => x.ValidationState == ModelValidationState.Invalid)
               .SelectMany(x => x.Errors)
               .Select(x => x.ErrorMessage)
               .ToList();

            return string.Join($"{Environment.NewLine}", messages);
        }
    }
}
