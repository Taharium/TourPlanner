using System;
using Models;

namespace Tour_Planner.Stores.TourStores;

public interface ITourStore {
    event Action<Tour?>? OnSelectedTourChangedEvent;
    Tour? CurrentTour { get; }
    void SetCurrentTour(Tour? tour);
}