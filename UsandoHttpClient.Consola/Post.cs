using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using UsandoHttpClient.Shared.Utilidades;

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
                TemperatureC = 90,
                //Summary = "Nueva temperatura"
            };

            #region 1.- realizar un PostAsJsonAsync
            //var resp = await http.PostAsJsonAsync(url, DTO);
            //var cuerpo = await resp.Content.ReadAsStringAsync();
            //if (resp.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"RESPUESTA DEL POST CON EXITO: {cuerpo}");
            //}
            //else
            //{
            //    Console.WriteLine($"RESPUESTA DEL POST CON ERROR: {cuerpo}");
            //}
            //Console.WriteLine($"Codigo de estado: {(int)resp.StatusCode} ({resp.StatusCode})");
            //Console.WriteLine("\n");
            #endregion

            #region 2.- realizar un PostAsync
            //var jsonString = JsonSerializer.Serialize(DTO); //Serializar el objeto DTO a JSON
            //var content = new StringContent(jsonString, 
            //                                Encoding.UTF8, 
            //                                "application/json"); // creamos el contenido de la solicitud HTTP con el JSON serializado

            //var resp = await http.PostAsync(url, content); // enviamos la solicitud POST al servidor
            //var cuerpo = await resp.Content.ReadAsStringAsync(); // leemos la respuesta del servidor como una cadena de texto

            //if(resp.IsSuccessStatusCode)
            //{
            //    Console.WriteLine($"RESPUESTA DEL POST CON EXITO: {cuerpo}");
            //}
            //else
            //{
            //    Console.WriteLine($"RESPUESTA DEL POST CON ERROR: {cuerpo}");
            //}
            //Console.WriteLine($"Codigo de estado: {(int)resp.StatusCode} ({resp.StatusCode})");
            #endregion

            #region 3.- Controlar errores con DataNotations
            var jsonString = JsonSerializer.Serialize(DTO); //Serializar el objeto DTO a JSON
            var content = new StringContent(jsonString,
                                            Encoding.UTF8,
                                            "application/json"); // creamos el contenido de la solicitud HTTP con el JSON serializado

            var resp = await http.PostAsync(url, content);

            var cuerpo = await resp.Content.ReadAsStringAsync(); // leemos la respuesta del servidor como una cadena de texto

            if (resp.IsSuccessStatusCode)
            {
                Console.WriteLine($"RESPUESTA DEL POST CON EXITO: {cuerpo}");
            }
            else if (resp.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                Console.WriteLine($"RESPUESTA DEL POST CON ERROR: {cuerpo}");
                Console.WriteLine($"Codigo de estado: {(int)resp.StatusCode} ({resp.StatusCode})");
                Console.WriteLine("\n");

                var errores = Util.ExtraerErroresDelWebApi(cuerpo);
                foreach (var campo in errores)
                {
                    Console.WriteLine($"Campo: {campo.Key}");
                    foreach (var msg in campo.Value)
                    {
                        Console.WriteLine($" MENSAJE - {msg}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"RESPUESTA DEL POST CON ERROR: {cuerpo}");
                Console.WriteLine($"Codigo de estado: {(int)resp.StatusCode} ({resp.StatusCode})");
            }
            #endregion


            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }
    }
}
