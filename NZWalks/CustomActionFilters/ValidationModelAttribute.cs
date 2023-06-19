using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.CustomActionFilters
{
    public class ValidationModelAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
           if(!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors)
                                                       .Select(e => e.ErrorMessage)
                                                       .ToList();
                context.Result = new BadRequestObjectResult(errors);
            }
        }
    }
}
