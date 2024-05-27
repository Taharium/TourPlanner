using DataAccessLayer.TourLogsRepository;
using DataAccessLayer.ToursRepository;

namespace DataAccessLayer;

public class UnitofWorkFactory : IUnitofWorkFactory
{
    private readonly Func<IToursRepository> _toursRepository;
    
    private readonly Func<ITourLogsRepository> _tourLogsRepository;
    
    private readonly Func<TourPlannerDbContext> _context;

    public UnitofWorkFactory(Func<IToursRepository> CreateTourRepository, Func<ITourLogsRepository> CreateTourLogsRepository, 
        Func<TourPlannerDbContext> CreateDbContext)
    {
        _toursRepository = CreateTourRepository;
        _tourLogsRepository = CreateTourLogsRepository;
        _context = CreateDbContext;
    }
    
    public IUnitofWork CreateUnitofWork()
    {
        return new UnitofWork(_toursRepository(), _tourLogsRepository(), _context());
    }

    
}