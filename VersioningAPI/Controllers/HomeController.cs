using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersioningAPI.Controllers
{
    // url: api/home
    // url: api/v1.0/home
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0", Deprecated = true)]
    //[ApiVersion("2.0", Deprecated= true)] 
    public class HomeController : ControllerBase
    {
        [HttpGet(template: "greeting")]
        [MapToApiVersion("1.0")]
        public string GetGreeting()
        {
            return "Hello World!";
        }
        // GET: http://localhost:xxxx/api/home
        [HttpGet(template: "today")]
        [MapToApiVersion("1.0")]
        public string GetToday()
        {
            return "Today is " + System.DateTime.Now.ToString();
        }
        //URL: http://localhost:xxxx/api/v2.0/home/tomorrow - works
        //URL: http://localhost:xxxx/api/home/tomorrow - not works -404
        //URL: http://localhost:xxxx/api/v1.0/home/tomorrow - not works -404
        [HttpGet(template: "tomorrow")]
        [MapToApiVersion("2.0")]
        public string GetTomorrow()
        {
            return "Tomorrow is " + System.DateTime.Now.AddDays(1).ToString();
        }

    }
}
