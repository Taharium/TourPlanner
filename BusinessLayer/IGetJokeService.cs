using Models;

namespace BusinessLayer;

public interface IGetJokeService {
    Task<Joke> GetJoke();
}