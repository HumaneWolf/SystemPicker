using System.Collections.Generic;
using System.Linq;
using SystemPicker.Matcher.Finders;
using Microsoft.AspNetCore.Mvc;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class NamedSystemsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        public IActionResult Get(int page = 1, int pageSize = 500)
        {
            if (pageSize > 5000) pageSize = 5000;
            var result = NamedSystemFinder.Systems
                .Skip(pageSize * (page - 1)).Take(pageSize).ToList();
            return Ok(result);
        }
    }
}