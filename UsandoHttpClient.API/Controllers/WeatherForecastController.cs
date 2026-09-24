using Microsoft.AspNetCore.Mvc;

namespace UsandoHttpClient.Shared.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "pepe", "Chilly", "juan", "Mild", "Warm", "que se yo", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public ActionResult<IEnumerable<WeatherForecast>> Get()
        {
            //return BadRequest();
            HttpContext.Response.Headers.Add("NuevoEncabezado", "El valor si puede tener espacios");
            
            var resp = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return resp;
        }

        [HttpPost]
        public ActionResult<int> Post(WeatherForecast DTO)
        {
            //Console.WriteLine($"Ejecución del Post<{DTO.Summary}>");
            //return 1;
            Console.WriteLine($"Ejecución del Post con error<{DTO.Summary}>");
            return BadRequest();
        }
    }
}
