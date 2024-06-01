using BusinessLayer.BLException;
using BusinessLayer.Extensions;
using Models;
using Newtonsoft.Json.Linq;

namespace BusinessLayer;

public class BusinessLogicOpenWeatherService : IOpenWeatherService {
    private readonly string _weatherApiKey;
    private readonly HttpClient _httpClient;
    private  string _lat = "";
    private string _lon = "";
    
    //TODO: add Logging

    public BusinessLogicOpenWeatherService(IConfigOpenWeatherService openWeatherService) {
        
        try {
            _httpClient = new HttpClient {
                BaseAddress = new Uri("https://api.openweathermap.org")
            };
        }
        catch (Exception) {
            throw new BusinessLayerException("Could not set base address for OpenWeatherService: https://api.openweathermap.org!");
        }
        _weatherApiKey = openWeatherService.WeatherApiKey;
    }
    
    public async Task<List<Weather>> GetWeather(List<string> coordinates) {
        try {
            _lat = coordinates[0];
            _lon = coordinates[1];
            var response = await _httpClient.GetAsync($"/data/2.5/forecast?lat={_lat}&lon={_lon}&appid={_weatherApiKey}&units=metric&cnt=4");
            var content = await response.Content.ReadAsStringAsync();
            JObject jObject = JObject.Parse(content);
            JArray weatherList = JArray.Parse(jObject["list"].Serialize());
            
            List<Weather> weathers = new List<Weather>();
            foreach (var weather in weatherList) {
                var datetime = DateTime.Parse($"{weather["dt_txt"]}").ToString("dd/MM/yyyy HH:mm"); 
                weathers.Add(new Weather() {
                    Date = datetime.Split(" ", StringSplitOptions.RemoveEmptyEntries)[0],
                    Time = datetime.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1],
                    Icon = $"../Assets/Weather_icons/{weather["weather"]?[0]?["icon"]}.png",
                    Description = $"{weather["weather"]?[0]?["description"]}",
                    Temp = $"{weather["main"]?["temp"]}°C",
                });
            }
            
            return weathers;
        }
        catch (Exception) {
            throw new BusinessLayerException($"Could not get the Weather for the specified location using the Geocoordinates: lat: {_lat}, lon: {_lon}!");
        }
        
        
    }
}