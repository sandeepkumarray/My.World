using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace My.World.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ValuesController()
        {

        }

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            return new string[] { "value1", "value2", "Environment" , webHostEnvironment.EnvironmentName };
        }
        [HttpPost]
        [Route("Fields/{data}")]
        public string GetCanvasFields([FromBody] List<string> canvasFields, string data)
        {
            return "Ok : " + data + " " + String.Join(",",canvasFields);
        }

    }
}
