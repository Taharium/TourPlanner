using DataAccessLayer.DBContextFactory;
using DataAccessLayer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.TourLogsRepository;

public class TourLogsRepository : ITourLogsRepository
{
    private readonly TourPlannerDbContext _context;

    public TourLogsRepository(TourPlannerDbContext dbContext)
    {
        _context = dbContext;
    }


    //public IEnumerable<TourLogsDTO> GetTourLogs(TourDTO tourDTO) => _context.TourLogs.Where(t => t.TourId == tourDTO.Id).ToList();

    public void AddTourLog(TourLogsDTO tourLogsDTO)
    {
        _context.TourLogs.Add(tourLogsDTO);
    }

    public void DeleteTourLog(TourLogsDTO tourLogDTO)
    {
        _ = _context.TourLogs.Find(tourLogDTO.Id) ?? throw new ArgumentException("TourLog not found with this Id");
        
        _context.TourLogs.Remove(tourLogDTO);
    }

    public void UpdateTourLog(TourLogsDTO tourLogDTO) {

        _context.Entry(tourLogDTO).State = EntityState.Modified;
    }
}