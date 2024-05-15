﻿using DataAccessLayer.TourLogsRepository;
using DataAccessLayer.ToursRepository;

namespace DataAccessLayer;

public interface IUnitofWork
{
    public IToursRepository ToursRepository { get; }
    
    public ITourLogsRepository TourLogsRepository { get; }

    Task<int> Commit();

}