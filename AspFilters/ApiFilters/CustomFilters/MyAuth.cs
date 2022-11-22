using Microsoft.AspNetCore.Authorization;

namespace ApiFilters.CustomFilters
{
    public class MyAuthAttribute : AuthorizeAttribute
    {
        public MyAuthAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}
