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
        private readonly IWebHostEnvironment env;

        public WeatherForecastController(IWebHostEnvironment env)
        {
            this.env = env;
        }

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
            Console.WriteLine($"Ejecución del Post<{DTO.Summary}>");
            return 1;
            //Console.WriteLine($"Ejecución del Post con error<{DTO.Summary}>");
            //return BadRequest();
        }

        [HttpPost("subirarchivo")]
        public async Task<ActionResult> SubirArchivo([FromForm] IFormFile archivo)
        {
            string nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";
            string rutaCarpetaDocumentos = Path.Combine(env.WebRootPath, "Documentos");

            if (!Directory.Exists(rutaCarpetaDocumentos))
            {
                Directory.CreateDirectory(rutaCarpetaDocumentos);
            }

            string rutaArchivo = Path.Combine(rutaCarpetaDocumentos, nombreArchivo);

            using (var fs = System.IO.File.Create(rutaArchivo))
            {
                await archivo.OpenReadStream().CopyToAsync(fs);
            }

            //var fs = System.IO.File.Create(rutaArchivo);
            //await archivo.OpenReadStream().CopyToAsync(fs);
            //fs.DisposeAsync();

            return Ok();
        }
    }
}
