using System;
using System.Collections.Generic;
using System.Text;

namespace UsandoHttpClient.Consola
{
    public static class ManejoDeArchivo
    {
        public static async Task Subir()
        {
            Console.WriteLine("Subiendo archivo al servidor");
            Console.WriteLine("\n");

            var http = new HttpClient();
            var url = "https://localhost:7066/WeatherForecast";

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

        public static async Task Descargar()
        {
            Console.WriteLine("Descargando archivo del servidor");
            Console.WriteLine("\n");

            var http = new HttpClient();

            #region 1.- Preparando datos del archivo
            var urlArchivoQueSeBaja = "https://localhost:7066/Documentos/718ed2c7-5049-4b1c-a3c6-7964b1b159c0.txt";
            var ArchivoQueSeGuarda = $@"D:\xx\ManejoArchivo\Pepe {DateTime.Now:yyyyMMdd_HHmmss}.txt";
            #endregion

            var stream = await http.GetStreamAsync(urlArchivoQueSeBaja);

            using (var fs = File.Create(ArchivoQueSeGuarda))
            {
                await stream.CopyToAsync(fs);
            }
        }

    }
}
