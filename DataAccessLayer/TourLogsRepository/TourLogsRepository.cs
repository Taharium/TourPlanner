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


    public IEnumerable<TourLogsDTO> GetTourLogs(TourDTO tourDTO) => _context.TourLogs.Where(t => t.TourId == tourDTO.Id).ToList();

    public void AddTourLog(TourLogsDTO tourLogsDTO)
    {
        _context.TourLogs.Add(tourLogsDTO);
    }

    public void DeleteTourLog(TourLogsDTO tourLogDTO)
    {
        _ = _context.TourLogs.Find(tourLogDTO.Id) ?? throw new ArgumentException("TourLog not found with this Id");
        
        _context.TourLogs.Remove(tourLogDTO);
    }

    public void UpdateTourLog(TourLogsDTO tourLogDTO)
    {
        var entry = _context.TourLogs.Find(tourLogDTO.Id) ?? throw new ArgumentException("TourLog not found with this Id");

        _context.Entry(entry).CurrentValues.SetValues(tourLogDTO);
    }
}