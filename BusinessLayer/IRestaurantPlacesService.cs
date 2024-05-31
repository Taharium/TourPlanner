namespace BusinessLayer;

public interface IRestaurantPlacesService {
    Task<List<string>> GetRestaurantRecommendations(List<string> coordinates);
}