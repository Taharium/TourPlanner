using DataAccessLayer.DBContextFactory;
using DataAccessLayer.DTOs;

namespace DataAccessLayer.TourLogsRepository;

public class TourLogsRepository : ITourLogsRepository
{
    private readonly TourPlannerDbContext _context;

    public TourLogsRepository(ITourPlannerDbContextFactory contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }
    
    
    public void AddTourLog(TourLogsDTO tourLogsDTO)
    {
        _context.TourLogs.Add(tourLogsDTO);
    }
}