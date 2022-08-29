using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;

namespace V_secretary_bot
{
    public class NasaGeter
    {
        public static readonly HttpClient clientNasa = new HttpClient();
        public static async Task<Nasa> ProcessNasa()
        {
            clientNasa.DefaultRequestHeaders.Accept.Clear();
            clientNasa.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            clientNasa.DefaultRequestHeaders.Add("User-Agent", ".NET");
            var stringTask = clientNasa.GetStringAsync("https://api.nasa.gov/planetary/apod?api_key=AjLn5gsRl1BLISwVkKGhIcsnXNaIr8xdtPy8cS3l");
            var responce = JsonConvert.DeserializeObject<Nasa>(await stringTask);

            return responce;

        }

    }
}
