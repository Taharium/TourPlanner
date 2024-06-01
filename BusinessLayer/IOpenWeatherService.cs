using Models;

namespace BusinessLayer;

public interface IOpenWeatherService {
    Task<List<Weather>> GetWeather(List<string> coordinates);
}