using DataAccessLayer.DALException;
using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.TourLogRepository;

//TODO: add Logging
public class TourLogsRepository : ITourLogsRepository
{
    private readonly TourPlannerDbContext _context;

    public TourLogsRepository(TourPlannerDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public void AddTourLog(TourLogsDTO tourLogDTO)
    {
        try {
            _context.TourLogs.Add(tourLogDTO);
        }
        catch (Exception) {
             throw new DataLayerException($"Failed to add TourLog to Tour with Name: {tourLogDTO.Tour.Name}" +
                                                 $" and ID: {tourLogDTO.Tour.Id}");
        }
    }

    public void DeleteTourLog(TourLogsDTO tourLogDTO)
    {
        try {
            _context.TourLogs.Remove(tourLogDTO);
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to remove TourLog with ID {tourLogDTO.Id} from Tour with " +
                                         $"Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}");
        }
    }

    public void UpdateTourLog(TourLogsDTO tourLogDTO) {
        try {
            _context.Entry(tourLogDTO).State = EntityState.Modified;
        }
        catch (Exception) {
            throw new DataLayerException($"Failed to update TourLog with ID: {tourLogDTO.Id} from Tour with " +
                                         $"Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}");
        }
    }
}