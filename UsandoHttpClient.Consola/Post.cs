using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace UsandoHttpClient.Consola
{
    public static class Post
    {
        public static async Task Ejecutar()
        {
            Console.WriteLine(" INICIANDO APLICACIÓN POST...");
            Console.WriteLine("\n");

            var http = new HttpClient();
            var url = "https://localhost:7056/WeatherForecast";
            var DTO = new Shared.WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 25,
                Summary = "Calor"
            };

            #region 1.- realizar un PostAsJsonAsync
            var resp = await http.PostAsJsonAsync(url, DTO);
            var cuerpo = await resp.Content.ReadAsStringAsync();
            if (resp.IsSuccessStatusCode)
            {
                Console.WriteLine($"RESPUESTA DEL POST CON EXITO: {cuerpo}");
            }
            else
            {
                Console.WriteLine($"RESPUESTA DEL POST CON ERROR: {cuerpo}");
            }
            Console.WriteLine($"Codigo de estado: {(int)resp.StatusCode} ({resp.StatusCode})");
            Console.WriteLine("\n");
            #endregion

            #region 2.- realizar un PostAsync
            #region

            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }
    }
}
