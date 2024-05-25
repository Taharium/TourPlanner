using DataAccessLayer.DBContextFactory;
using DataAccessLayer.TourLogsRepository;
using DataAccessLayer.ToursRepository;

namespace DataAccessLayer;

public class UnitofWork : IUnitofWork, IDisposable
{
    public IToursRepository ToursRepository { get; }
    
    public ITourLogsRepository TourLogsRepository { get; }
    
    private TourPlannerDbContext _context;

    private readonly ITourPlannerDbContextFactory _contextFactory;
    
    private bool _disposed = false;
    
    public UnitofWork(IToursRepository toursRepository, ITourLogsRepository tourLogsRepository, ITourPlannerDbContextFactory contextFactory)
    {
        ToursRepository = toursRepository;
        TourLogsRepository = tourLogsRepository;
        _contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext();
    }
    
    public async Task<int> Commit()
    {
        var result = await _context.SaveChangesAsync();
        _context.Dispose();
        _context = _contextFactory.CreateDbContext();
        return result;
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