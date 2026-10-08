using System;
using System.Collections.Generic;
using System.Text;

namespace UsandoHttpClient.Consola
{
    public static class SubirArchivo
    {
        public static async Task Ejecutar()
        {
            Console.WriteLine("Subiendo archivo al servidor");
            Console.WriteLine("\n");

            var http = new HttpClient();
            var url = "https://localhost:7056/WeatherForecast";

            #region 1.- Prepara el achivo para subir
                string rutaArchivo = @"D:\xx\ManejoArchivo\ArchivoASubir.txt";
                string nombreArchivo = Path.GetFileName(rutaArchivo);
                var fs = System.IO.File.OpenRead(rutaArchivo);
            #endregion

            #region 2.- Subiendo el achivo
                var contenido = new MultipartFormDataContent();
                var streamContent = new StreamContent(fs);

                contenido.Add(streamContent, "archivo", nombreArchivo);
                url = @$"{url}/subirarchivo";

                var resp = await http.PostAsync(url, contenido);
                resp.EnsureSuccessStatusCode();
            #endregion
        }

    }
}
