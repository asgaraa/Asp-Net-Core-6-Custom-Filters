using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using ServiceLayer.DTOs.UserDetail;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace ServiceLayer.Services
{
    public class UserInfoService :IUserInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public UserDetailDto GetUserInfo()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var useremail = user.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email).Value;

            return new UserDetailDto { Id = userId, Email = useremail };
        }

        public UserIdDto GetUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            var userId = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            return new UserIdDto { Id = userId };
        }
    }
}
