using System.Diagnostics;
using BusinessLayer.BLException;
using System.Text.Json.Nodes;

namespace BusinessLayer;

public class BusinessLogicRestaurantPlacesService : IRestaurantPlacesService {

    private readonly string _apiKey;
    private string _body = "";
    public BusinessLogicRestaurantPlacesService(IConfigRestaurantPlacesService configRestaurantPlacesService) {
        _apiKey = configRestaurantPlacesService.ResApiKey;
    }
    
    public async Task<List<string>> GetRestaurantRecommendations(List<string> coordinates) {
        try {
            string latitude = coordinates[0];
            string longitude = coordinates[1];
            string requestUri = $@"https://map-places.p.rapidapi.com/nearbysearch/json?location={latitude}%2C{longitude}&radius=500&type=restaurant\";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestUri),
                    Headers =
                    {
                        { "x-rapidapi-key", _apiKey },
                        { "x-rapidapi-host", "map-places.p.rapidapi.com" },
                    },
                };
                
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    _body = await response.Content.ReadAsStringAsync();
                }
                
            }
            var jsonNode = JsonNode.Parse(_body);
            //read the name property from every feature
            var places = new List<string>();
            // Check if the features property exists and is an array
            if (jsonNode?["results"] is JsonArray results) {
                // Iterate over each feature
                /*foreach (var result in results) {
                    // Check if the feature has a properties object and a name property
                    if (result?["properties"]?["label"] is { } labelNode && result?["properties"]?["continent"] is { } continentNode) {
                        string node = $"{labelNode}, {continentNode}";
                        places.Add(node);
                    }
                }*/
                Debug.WriteLine(results);
            }

            return places;
        }
        catch (Exception) {
            throw new BusinessLayerException("Could not get nearby restaurant recommendations");
        }
        
    }
}