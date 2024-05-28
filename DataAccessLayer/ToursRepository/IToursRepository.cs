using DataAccessLayer.DTOs;
using Models;

namespace DataAccessLayer.ToursRepository;

public interface IToursRepository {
    TourDTO? GetById(int id);
    IEnumerable<TourDTO> GetTours();
    void AddTour(TourDTO tour);
    void DeleteTour(TourDTO tour);
    void UpdateTour(TourDTO tour);
    
}