using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace UsandoHttpClient.Shared.Utilidades
{
    public static class Util
    {
        public static Dictionary<string, List<string>> ExtraerErroresDelWebApi(string json)
        {
            var respuesta = new Dictionary<string, List<string>>();

            var deserializado = JsonSerializer.Deserialize<JsonElement>(json);
            var campoerrores = deserializado.GetProperty("errors");

            foreach(var campo in campoerrores.EnumerateObject())
            {
                var nombreCampo = campo.Name;
                var errores = new List<string>();
                foreach (var error in campo.Value.EnumerateArray())
                {
                    errores.Add(error.ToString());
                }
                //respuesta[nombreCampo] = errores;
                respuesta.Add(nombreCampo, errores);
            }

            return respuesta;
        }
    }
}
