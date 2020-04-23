using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using $safeprojectname$._.Controllers;
using $safeprojectname$._.Problems;
using $safeprojectname$.Resources;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    [Authorize]
    [Route("sample")]
    public class SampleController : ApiControllerBase<SampleController>
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public SampleController(IStringLocalizer<Resource> localizer, ILogger<SampleController> logger) : base(localizer, logger)
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rng = new Random();
            var a =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return ActionResult.Ok(a);
        }

        [HttpGet]
        [Route("exception")]
        public IActionResult GetException()
        {
            throw new ArgumentNullException();
        }

        [HttpGet]
        [Route("notfound")]
        public IActionResult GetNotFound()
        {
            return ActionResult.NotFound(new ResourceNotFoundProblemDetails(localizer[Resource.generic_not_found_details],localizer));
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}
