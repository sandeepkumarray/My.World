using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using My.World.Web.Models;

namespace My.World.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string Domain = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host.Host + (Request.Host.Port == null ? "" : ":" + Request.Host.Port);
            ViewBag.Domain = Domain;
            return View();
        }

        // GET: api/<ValuesController>
        [HttpGet("values")]
        public IEnumerable<string> Get(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            return new string[] { "value1", "value2", "Environment", webHostEnvironment.EnvironmentName };
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(
        [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (context != null)
            {
                if (context.Error != null)
                {
                    if (context.Error.GetType() == typeof(InvalidOperationException))
                    {
                        return View(new ErrorViewModel
                        {
                            StatusCode = "404",
                            Message = "Oops! You're lost. The page you are looking for was not found.",
                            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                            Environment = webHostEnvironment.EnvironmentName
                        });
                    }
                }
                _logger.LogError(context.Error?.Message);
            }
            //return Problem(
            //    detail: context.Error.StackTrace,
            //    title: context.Error.Message);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Environment = webHostEnvironment.EnvironmentName });
        }
    }
}
