using DataAccessLayer.TourLogRepository;
using DataAccessLayer.TourRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class UnitofWork : IUnitofWork, IDisposable
{
    public IToursRepository ToursRepository { get; }
    
    public ITourLogsRepository TourLogsRepository { get; }

    private TourPlannerDbContext _context;
    
    private bool _disposed = false;
    
    public UnitofWork(IDbContextFactory<TourPlannerDbContext> contextFactory) {
        _context = contextFactory.CreateDbContext();

        ToursRepository = new ToursRepository(_context);
        TourLogsRepository = new TourLogsRepository(_context);
    }
    
    public async Task<int> Commit()
    {
        var result = await _context.SaveChangesAsync();
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