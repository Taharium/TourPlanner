using DataAccessLayer.TourLogRepository;
using DataAccessLayer.TourRepository;

namespace DataAccessLayer;

public interface IUnitofWork : IDisposable
{
    public IToursRepository ToursRepository { get; }
    
    public ITourLogsRepository TourLogsRepository { get; }

    Task<int> Commit();

}