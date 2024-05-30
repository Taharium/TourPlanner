using DataAccessLayer.DTOs;

namespace DataAccessLayer.TourRepository;

public interface IToursRepository {
    TourDTO GetById(int tour, string name);
    IEnumerable<TourDTO> GetTours();
    void AddTour(TourDTO tour);
    void DeleteTour(TourDTO tour);
    void UpdateTour(TourDTO tour);
    
}