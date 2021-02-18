using System.Collections.Generic;
using SystemPicker.Matcher.Finders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class NamedSectorsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        [SwaggerOperation(
            Summary = "Get a list of know named sectors.",
            Description = "Returns a lowercase list of known named sectors, as is used by the system."
        )]
        public IActionResult Get()
        {
            return Ok(NamedSectorFinder.SectorList);
        }
    }
}