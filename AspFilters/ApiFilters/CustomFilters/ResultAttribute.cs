

using DomainLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiFilters.CustomFilters
{
    public class ResultAttribute: ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if (result?.Value is AppUser value)
                result.Value = new
                {
                    Id = value.Id,
                    Username = value.UserName,
                    Fullname = value.FullName,
                    Mobile = value.PhoneNumber,
                    Email = value.Email,
                 
                };
            await next();
        }
    }
}
