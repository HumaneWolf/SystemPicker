using SystemPicker.Matcher.Finders;
using SystemPicker.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClassifySystemController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ClassifyOutput), 200)]
        [SwaggerOperation(
            Summary = "Classify the system name category.",
            Description = "Find which set of naming rules we recognize the system as. Result: ProcGen/Catalog/ProcGenNamedSector/Named/Unknown."
        )]
        public IActionResult Get(string name)
        {
            if (ProcGenFinder.IsProcGen(name))
            {
                return Ok(new ClassifyOutput("ProcGen"));
            }
            if (CatalogFinder.IsCatalogSystem(name))
            {
                return Ok(new ClassifyOutput("Catalog"));
            }
            if (NamedSectorFinder.ExtractSectorName(name) != null)
            {
                return Ok(new ClassifyOutput("ProcGenNamedSector"));
            }
            if (NamedSystemFinder.IsNamedSystem(name))
            {
                return Ok(new ClassifyOutput("Named"));
            }
            return Ok(new ClassifyOutput("Unknown"));
        }
    }
}