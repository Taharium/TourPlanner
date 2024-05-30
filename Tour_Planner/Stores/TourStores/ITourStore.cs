using System;
using System.Collections.ObjectModel;
using Models;

namespace Tour_Planner.Stores.TourStores;

public interface ITourStore {
    ObservableCollection<Tour> Tours { get; set; }
    event Action<Tour?>? OnSelectedTourChangedEvent;
    event Action<Tour?>? OnTourDeleteEvent;
    public event Action<Tour?>? OnTourAddedEvent;

    Tour? CurrentTour { get; }
    void SetCurrentTour(Tour? tour);
}