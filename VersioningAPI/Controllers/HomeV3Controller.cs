using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersioningAPI.Controllers
{
    [ApiController]
    [ControllerName("Home")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("3.0")]
    public class HomeV3Controller : ControllerBase
    {
        [HttpGet(template: "welcome/{name}")]
        [MapToApiVersion("3.0")]
        [Obsolete("This method is deptrecated. Use Another method.", true)]
        public IActionResult Welcome(String Name)=> Ok($"Welcome {Name}");

    }
}
