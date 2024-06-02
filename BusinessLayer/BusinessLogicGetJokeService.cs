using BusinessLayer.BLException;
using DataAccessLayer.Logging;
using Models;
using Newtonsoft.Json.Linq;

namespace BusinessLayer;

public class BusinessLogicGetJokeService : IGetJokeService {

    private readonly HttpClient _httpClient;
    private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();

    public BusinessLogicGetJokeService() {
        try {
            _httpClient = new HttpClient() {
                BaseAddress = new Uri("https://official-joke-api.appspot.com")
            };
        }
        catch (Exception) {
            Logger.Error("Could not set base address for the Joke API: https://official-joke-api.appspot.com!");
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
            Logger.Error("Could not get a Joke from https://official-joke-api.appspot.com/jokes/programming/random!");
            throw new BusinessLayerException("Could not get a Joke from https://official-joke-api.appspot.com/jokes/programming/random!");
        }
    }
}