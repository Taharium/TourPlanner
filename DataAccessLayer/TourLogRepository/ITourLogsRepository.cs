using DataAccessLayer.DTOs;

namespace DataAccessLayer.TourLogRepository;

public interface ITourLogsRepository
{
     void AddTourLog(TourLogsDTO tourLogDTO);
     void DeleteTourLog(TourLogsDTO tourLogDTO);
     void UpdateTourLog(TourLogsDTO tourLogDTO);
}