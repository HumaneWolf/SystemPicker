using System.Collections.Generic;
using SystemPicker.Matcher.Finders;
using SystemPicker.Matcher.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class StarCataloguesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<StarCatalog>), 200)]
        [SwaggerOperation(
            Summary = "Get a list of known star catalogues.",
            Description = "Known name patterns, along with examples, and if available the name of the catalog."
        )]
        public IActionResult Get()
        {
            return Ok(CatalogFinder.Catalogs);
        }
    }
}