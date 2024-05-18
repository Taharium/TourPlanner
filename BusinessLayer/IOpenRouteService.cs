using Models;

namespace BusinessLayer;

public interface IOpenRouteService
{
    Task<List<string>> GetGeoCoordinates(string location);
    
    Task<string> GetRoute(Tour tour);
}