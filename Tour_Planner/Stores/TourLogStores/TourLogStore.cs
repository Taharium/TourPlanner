using System;
using Models;

namespace Tour_Planner.Stores.TourLogStores;

public class TourLogStore : ITourLogStore {
    public TourLogs? CurrentTour { get; private set; }

    public event Action<TourLogs?>? OnSelectedTourChangedEvent;

    private void OnSelectedTourChange() {
        OnSelectedTourChangedEvent?.Invoke(CurrentTour);
    }

    public void SetCurrentTour(TourLogs? tour) {
        CurrentTour = tour;
        OnSelectedTourChange();
    }
}