using System;
using Models;

namespace Tour_Planner.Stores.TourLogStores;

public interface ITourLogStore {
    event Action<TourLogs?>? OnSelectedTourChangedEvent;
    TourLogs? CurrentTour { get; }
    void SetCurrentTourLog(TourLogs? tour);
}