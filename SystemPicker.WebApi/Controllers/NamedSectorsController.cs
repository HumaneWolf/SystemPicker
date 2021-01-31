using System.Collections.Generic;
using SystemPicker.Matcher.Finders;
using Microsoft.AspNetCore.Mvc;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class NamedSectorsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        public IActionResult Get()
        {
            return Ok(NamedSectorFinder.SectorList);
        }
    }
}