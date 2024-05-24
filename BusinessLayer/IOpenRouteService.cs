using System.Text.Json.Nodes;
using Models.Enums;

namespace BusinessLayer;

public interface IOpenRouteService
{
    Task<List<string>> GetGeoCoordinates(string location);
    
    string GetDistance(JsonNode jsonNodeDirections);
    
    string GetEstimatedTime(JsonNode jsonNodeDirections);
    
    Task<List<string>> GetPlaces(string location);
    
    Task<JsonNode> GetRoute(string startLocation, string endLocation, TransportType transportType);
}