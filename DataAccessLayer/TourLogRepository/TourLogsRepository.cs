using DataAccessLayer.DALException;
using DataAccessLayer.DTOs;
using DataAccessLayer.Logging;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.TourLogRepository;

public class TourLogsRepository : ITourLogsRepository
{
    private readonly TourPlannerDbContext _context;
    private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();
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
            Logger.Fatal($"Failed to add TourLog to Tour with Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}!");
             throw new DataLayerException($"Database: Failed to add TourLog to Tour with Name: {tourLogDTO.Tour.Name}" +
                                                 $" and ID: {tourLogDTO.Tour.Id}! Please connect to the database!");
        }
    }

    public void DeleteTourLog(TourLogsDTO tourLogDTO)
    {
        try {
            _context.TourLogs.Remove(tourLogDTO);
        }
        catch (Exception) {
            Logger.Fatal($"Failed to remove TourLog with ID {tourLogDTO.Id} from Tour with Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}!");
            throw new DataLayerException($"Database: Failed to remove TourLog with ID {tourLogDTO.Id} from Tour with " +
                                         $"Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}! Please connect to the database!");
        }
    }

    public void UpdateTourLog(TourLogsDTO tourLogDTO) {
        try {
            _context.Entry(tourLogDTO).State = EntityState.Modified;
        }
        catch (Exception) {
            Logger.Fatal($"Failed to update TourLog with ID: {tourLogDTO.Id} from Tour with Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}!");
            throw new DataLayerException($"Database: Failed to update TourLog with ID: {tourLogDTO.Id} from Tour with " +
                                         $"Name: {tourLogDTO.Tour.Name} and ID: {tourLogDTO.Tour.Id}! Please connect to the database!");
        }
    }
}