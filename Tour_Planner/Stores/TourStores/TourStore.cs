using System;
using Models;

namespace Tour_Planner.Stores.TourStores;

public class TourStore : ITourStore {
    public Tour? CurrentTour { get; private set; }

    public event Action<Tour?>? OnSelectedTourChangedEvent;

    private void OnSelectedTourChange() {
        OnSelectedTourChangedEvent?.Invoke(CurrentTour);
    }

    public void SetCurrentTour(Tour? tour) {
        CurrentTour = tour;
        OnSelectedTourChange();
    }
}