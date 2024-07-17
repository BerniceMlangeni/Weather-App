using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Weather_App.Models;

namespace Weather_App.Services
{
    public static class ApiService
    {
        public static async Task<Root> GetWeather(double Latitude, double Longitude)
        {
            var httpClient = new HttpClient();
           var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=000dd21625b9421ddc65cefb80266194", Latitude, Longitude));
           // return JsonConverter.DeserializeObject<Root>(response);
            return JsonConvert.DeserializeObject<Root>(response);

        }

         public  static async Task<Root> GetWeatherByCity(string city)
        {
            var httpClient = new HttpClient();
           var response = await httpClient.GetStringAsync(string.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=000dd21625b9421ddc65cefb80266194", city));
           // return JsonConverter.DeserializeObject<Root>(response);
            return JsonConvert.DeserializeObject<Root>(response);

        }
    }
}
