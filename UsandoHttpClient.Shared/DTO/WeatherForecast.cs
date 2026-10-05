using System.ComponentModel.DataAnnotations;

namespace UsandoHttpClient.Shared
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        //[Range(-20, 55)]
        [Range(-20, 55, ErrorMessage = "La temperatura debe estar entre -20 y 55 grados Celsius.")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Required(ErrorMessage = "El campo Summary es obligatorio.")]
        public string? Summary { get; set; } = null;
    }
}
