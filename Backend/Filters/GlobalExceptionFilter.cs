using ServiceStack;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace TimeTable_api
{
    // filter that catch exception globaly 
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var ex = context.Exception;

            // Log full exception
            Debug.WriteLine("--------------------------------------------------------------------------------");
            Debug.WriteLine(ex.ToString());
            Debug.WriteLine(ex.GetStatus());
            Debug.WriteLine("--------------------------------------------------------------------------------");

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new
                {
                    Message = ex.Message,
                    ExceptionType = ex.GetType().FullName,
                    StackTrace = ex.StackTrace,
                    Source = ex.Source,
                    TargetSite = ex.TargetSite?.ToString(),
                    InnerException = ex.InnerException?.ToString(),
                    FullDetails = ex.ToString()
                }
            );
        }

    }
}