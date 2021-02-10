using System.Collections.Generic;
using SystemPicker.Matcher.Finders;
using SystemPicker.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ClassifySystemController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
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