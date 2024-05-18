using System.Diagnostics;
using System.Globalization;
using System.Text.Json.Nodes;
using Models;

namespace BusinessLayer;

public class BusinessLogicOpenRouteService : IOpenRouteService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public BusinessLogicOpenRouteService(IConfigOpenRouteService configOpenRouteService)
    {
        
        _httpClient = new HttpClient{ BaseAddress = new Uri("https://api.openrouteservice.org/")};
        _apiKey = configOpenRouteService.ApiKey;
    }


    public async Task<List<string>> GetGeoCoordinates(string location)
    {
        var response = await _httpClient.GetAsync($"geocode/search?api_key={_apiKey}&text={location}");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var jsonNode = JsonNode.Parse(content);
//Console.WriteLine(jsonNode);
        var geoCoordinates = jsonNode?["features"]?[0]?["geometry"]?["coordinates"];
        Console.WriteLine(geoCoordinates);
        double latitude = geoCoordinates?[0]?.GetValue<Double>() ?? Double.MinValue;
        double longitude = geoCoordinates?[1]?.GetValue<Double>() ?? Double.MinValue;
//number formatter
        var numberFormat = new NumberFormatInfo();
        numberFormat.NumberDecimalSeparator = ".";
        string longitudeStr = longitude.ToString(numberFormat);
        string latitudeStr = latitude.ToString(numberFormat);
        return [longitudeStr, latitudeStr];
    }

    public async Task<string> GetRoute(Tour tour)
    {
        var startcoordinates = await GetGeoCoordinates(tour.StartLocation);
        var endcoordinates = await GetGeoCoordinates(tour.EndLocation);

        var responseDirections = _httpClient
            .GetAsync($"/v2/directions/{tour.TransportType}?api_key={_apiKey}&start={startcoordinates[0]},{startcoordinates[1]}&end={endcoordinates[0]},{endcoordinates[1]}");
        responseDirections.Result.EnsureSuccessStatusCode();
        var contentDirections = await responseDirections.Result.Content.ReadAsStringAsync();
        var jsonNodeDirections = JsonNode.Parse(contentDirections);
        tour.Distance = jsonNodeDirections?["features"]?[0]?["properties"]?["segments"]?[0]?["distance"]?.GetValue<Double>() ?? 0;
        tour.EstimatedTime = jsonNodeDirections?["features"]?[0]?["properties"]?["segments"]?[0]?["duration"]?.GetValue<String>() ?? "";
        return jsonNodeDirections?["features"]?[0]?["geometry"]?["coordinates"]?.ToString() ?? "";
    }
}