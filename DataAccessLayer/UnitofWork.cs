using DataAccessLayer.TourLogsRepository;
using DataAccessLayer.ToursRepository;

namespace DataAccessLayer;

public class UnitofWork : IUnitofWork, IDisposable
{
    public IToursRepository ToursRepository { get; }
    
    public ITourLogsRepository TourLogsRepository { get; }
    
    private readonly TourPlannerDbContext _context;
    
    private bool _disposed = false;
    
    public UnitofWork(IToursRepository toursRepository, ITourLogsRepository tourLogsRepository, TourPlannerDbContext context)
    {
        ToursRepository = toursRepository;
        TourLogsRepository = tourLogsRepository;
        _context = context;
    }
    
    public async Task<int> Commit()
    {
        return await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}