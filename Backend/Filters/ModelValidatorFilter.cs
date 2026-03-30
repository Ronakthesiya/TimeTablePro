using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TimeTable_api
{
    /// <summary>
    /// actiuon filter for model validation
    /// </summary>
    public class ModelValidatorFilter : ActionFilterAttribute
    {
        /// <summary>
        /// override ActionExecution method
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // chech model is valid or not
            if (!actionContext.ModelState.IsValid)
            {
                // if model is not valid return bad request responce
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    new
                    {
                        Message = "Model validation failed",
                        Errors = actionContext.ModelState
                            .Where(ms => ms.Value.Errors.Count > 0)
                            .ToDictionary(
                                kv => kv.Key,
                                kv => kv.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            )
                    }
                );
            }
        }
    }
}