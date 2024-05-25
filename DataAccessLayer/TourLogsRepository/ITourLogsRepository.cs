using DataAccessLayer.DTOs;

namespace DataAccessLayer.TourLogsRepository;

public interface ITourLogsRepository
{
     void AddTourLog(TourLogsDTO tourLogsDTO);
     
}