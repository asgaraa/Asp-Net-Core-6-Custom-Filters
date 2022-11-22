




using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiFilters.CustomFilters
{
    public class LoggingAttribute : ActionFilterAttribute
    {
      //  private readonly ILogger<LoggingAttribute> _logger;
        public LoggingAttribute()
        {
           // _logger = logger;
        }

        public override void OnActionExecuting(HttpActionContext context)
        {
            // do something before the action executes
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", context.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");
           
        }

        public override  void OnActionExecuted(HttpActionExecutedContext context)
        {
            // do something after the action executes
            Trace.WriteLine(string.Format("Action Method {0} executed at {1}", context.ActionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

        }
    }
}
