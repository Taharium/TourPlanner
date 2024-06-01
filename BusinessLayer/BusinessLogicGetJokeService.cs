using BusinessLayer.BLException;
using BusinessLayer.Extensions;
using Models;
using Newtonsoft.Json.Linq;

namespace BusinessLayer;

public class BusinessLogicGetJokeService : IGetJokeService {

    //TODO: add Logging
    private readonly HttpClient _httpClient;
    public BusinessLogicGetJokeService() {
        try {
            _httpClient = new HttpClient() {
                BaseAddress = new Uri("https://official-joke-api.appspot.com")
            };
        }
        catch (Exception) {
            throw new BusinessLayerException("Could not set base address for the Joke API: https://official-joke-api.appspot.com!");
        }
        
    }
    
    public async Task<Joke> GetJoke() {
        try {
            var response = await _httpClient.GetAsync("/jokes/programming/random");
            var content = await response.Content.ReadAsStringAsync();
            JArray jokeArray = JArray.Parse(content);

            Joke newJoke = new Joke();
            foreach (var joke in jokeArray) {
                newJoke.SetUp = $"\"{joke["setup"]}\"";
                newJoke.PunchLine = $"\"{joke["punchline"]}\"";
            }

            return newJoke;
        }
        catch (Exception) {
            throw new BusinessLayerException("Could not get a Joke from https://official-joke-api.appspot.com/jokes/programming/random!");
        }
    }
}