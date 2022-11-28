using ApiFilters.CustomFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Services.Interfaces;

namespace ApiFilters.Controllers
{

    public class DateTimeController : BaseController
    {
        private readonly IDateTimeService _service;
       // private readonly ILogger<DateTimeController> _logger;
        public DateTimeController(IDateTimeService service)
        {
            _service = service;
          //  _logger = logger;
        }

        [HttpGet]
        [Route("GetDateTime")]
        //  [MyAuth(RoleConstants.SuperAdmin)]
        // [Log]
        // [Result]
        [CustomResourceFilter("X-Value", "Y-Value")]
        public IActionResult GetDateTime()
        {
            var result = _service.GetCurrentDateTime();
            return Ok(result);
        }
    }
}
