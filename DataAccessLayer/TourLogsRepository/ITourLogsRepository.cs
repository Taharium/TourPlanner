using DataAccessLayer.DTOs;

namespace DataAccessLayer.TourLogsRepository;

public interface ITourLogsRepository
{
     IEnumerable<TourLogsDTO> GetTourLogs(TourDTO tourDTO);
     void AddTourLog(TourLogsDTO tourLogDTO);
     void DeleteTourLog(TourLogsDTO tourLogDTO);
     void UpdateTourLog(TourLogsDTO tourLogDTO);
     
     
     
}