using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ApiFilters.CustomFilters
{
    public class ControllerFilterExample : Attribute, IActionFilter
    {
        public ControllerFilterExample()
        {

        }
        public Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            Trace.WriteLine(string.Format("Action Method {0} executing at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

            var result = continuation();

            result.Wait();

            Trace.WriteLine(string.Format("Action Method {0} executed at {1}", actionContext.ActionDescriptor.ActionName, DateTime.Now.ToShortDateString()), "Web API Logs");

            return result;
        }

        public bool AllowMultiple
        {
            get { return true; }
        }
    }
}
