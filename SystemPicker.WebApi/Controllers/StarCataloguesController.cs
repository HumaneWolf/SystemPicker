using System.Collections.Generic;
using SystemPicker.Matcher.Finders;
using SystemPicker.Matcher.Models;
using Microsoft.AspNetCore.Mvc;

namespace SystemPicker.WebApi.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class StarCataloguesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<StarCatalog>), 200)]
        public IActionResult Get()
        {
            return Ok(CatalogFinder.Catalogs);
        }
    }
}