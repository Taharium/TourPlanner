using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public class UnitofWorkFactory : IUnitofWorkFactory {
    private readonly IDbContextFactory<TourPlannerDbContext> _contextFactory;

    public UnitofWorkFactory(IDbContextFactory<TourPlannerDbContext> contextFactory) {
        _contextFactory = contextFactory;
    }
    
    public IUnitofWork CreateUnitofWork() {
        
        return new UnitofWork(_contextFactory);
    }

    
}