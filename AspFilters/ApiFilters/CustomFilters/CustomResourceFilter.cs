using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace ApiFilters.CustomFilters
{
    public class CustomResourceFilter: Attribute, IResourceFilter
    {
        private readonly string[] _headers;

        public CustomResourceFilter(params string[] headers)
        {
            _headers = headers;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (_headers == null) return;

            if (!_headers.All(m => context.HttpContext.Request.Headers.ContainsKey(m)))
            {
                context.Result = new JsonResult(
                    new { Error = "Headers is null" }
                )
                { StatusCode = 400 };
                ;
            }
        }
    }
}