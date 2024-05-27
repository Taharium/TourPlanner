using System.Globalization;
using System.Text;
using System.Text.Json.Nodes;
using TransportType = Models.Enums.TransportType;

namespace BusinessLayer;

public class BusinessLogicOpenRouteService : IOpenRouteService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public BusinessLogicOpenRouteService(IConfigOpenRouteService configOpenRouteService)
    {
        
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.openrouteservice.org")
        };
        _apiKey = configOpenRouteService.ApiKey;
    }


    public async Task<List<string>> GetGeoCoordinates(string location)
    {
        var response = await _httpClient.GetAsync($"/geocode/search?api_key={_apiKey}&text={location}&size=1");
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
        var longitudeStr = longitude.ToString(numberFormat);
        var latitudeStr = latitude.ToString(numberFormat);
        return [latitudeStr, longitudeStr];
    }
    
    public string GetDistance(JsonNode jsonNodeDirections)
    {
        const int tokilometer = 1000;
        var distance = jsonNodeDirections?["features"]?[0]?["properties"]?["segments"]?[0]?["distance"]?.GetValue<Double>() / tokilometer ?? 0;
        var firstDecimal = Math.Floor(distance * 10) / 10;
        distance = firstDecimal >= 5 ? Math.Ceiling(distance) : Math.Floor(distance);
        var numberFormat = new NumberFormatInfo();
        numberFormat.NumberDecimalSeparator = ".";
        return distance.ToString(numberFormat);
    }
    
    public string GetEstimatedTime(JsonNode jsonNodeDirections)
    {
        const int tohours = 3600;
        double estimatedtime = jsonNodeDirections?["features"]?[0]?["properties"]?["segments"]?[0]?["duration"]?.GetValue<Double>() / tohours ?? 0;
        var estimatedtimestr = estimatedtime.ToString("F2");
        return estimatedtimestr;
    }

    public async Task<List<string>> GetPlaces(string location)
    {
        var response = await _httpClient.GetAsync($"/geocode/autocomplete?api_key={_apiKey}&text={location}&size=5");
        var content = await response.Content.ReadAsStringAsync();
        var jsonNode = JsonNode.Parse(content);
        //read the name property from every feature
        var places = new List<string>();
        // Check if the features property exists and is an array
        if (jsonNode?["features"] is JsonArray features) {
            // Iterate over each feature
            foreach (var feature in features) {
                // Check if the feature has a properties object and a name property
                if (feature?["properties"]?["label"] is { } labelNode) {
                    places.Add(labelNode.ToString());
                }
            }
        }

        return places;
    }

    public async Task<JsonNode> GetRoute(string startLocation, string endLocation, TransportType transportType)
    {
        var startcoordinates = await GetGeoCoordinates(startLocation);
        var endcoordinates = await GetGeoCoordinates(endLocation);
        
        string transport = transportType switch
        {
            TransportType.Car=> "driving-car",
            TransportType.Cycling => "cycling-regular",
            TransportType.Foot => "foot-walking",
            TransportType.Wheelchair => "wheelchair",
            _ => "driving-car"
        };
        
        var responseDirections = await _httpClient
            .GetAsync($"/v2/directions/{transport}?api_key={_apiKey}&start={startcoordinates[0]},{startcoordinates[1]}&end={endcoordinates[0]},{endcoordinates[1]}");
        responseDirections.EnsureSuccessStatusCode();
        var contentDirections = await responseDirections.Content.ReadAsStringAsync();
        var jsonNodeDirections = JsonNode.Parse(contentDirections);
        //COULD BE NULL Should be handled
        return jsonNodeDirections!;
        
    }
}