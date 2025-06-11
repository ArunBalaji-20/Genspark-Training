using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BugTrackingAPI.Models.DTO;

namespace BugTrackingAPI.Misc
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            

            context.Result = new ObjectResult(new ProblemDetails
            {
                Status = 400,
                Title = "An  error occurred.",
                Detail = context.Exception.Message 
            })
            {
                StatusCode = 400
            };
        }
    }
}