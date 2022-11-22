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
        private readonly ILogger<DateTimeController> _logger;
        public DateTimeController(IDateTimeService service, ILogger<DateTimeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetDateTime")]
        [MyAuth(RoleConstants.SuperAdmin)]
        [Logging]
        public IActionResult GetDateTime()
        {
            var result = _service.GetCurrentDateTime();
            return Ok(result);
        }
    }
}
