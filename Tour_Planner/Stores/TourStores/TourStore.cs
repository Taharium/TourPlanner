using System;
using System.Collections.ObjectModel;
using BusinessLayer;
using Models;

namespace Tour_Planner.Stores.TourStores;

public class TourStore : ITourStore {
    public Tour? CurrentTour { get; private set; }

    public ObservableCollection<Tour> Tours { get; set; } = new();

    public event Action<Tour?>? OnSelectedTourChangedEvent;
    
    public event Action<Tour?>? OnTourDeleteEvent;

    private readonly IBusinessLogicTours _businessLogicTours;
    
    public TourStore(IBusinessLogicTours businessLogicTours) {
        _businessLogicTours = businessLogicTours;
        LoadTours();

        _businessLogicTours.AddTourEvent += AddTour;
        _businessLogicTours.OnTourDeleteEvent += DeleteTour;
        _businessLogicTours.OnTourUpdateEvent += EditTour;
    }
    
    private async void LoadTours() {
        var tours = await _businessLogicTours.GetTours();
        foreach (var tour in tours) {
            Tours.Add(tour);
        }
    }

    private void AddTour(Tour tour)
    {
        Tours.Add(tour);
    }
    
    private void DeleteTour(Tour tour)
    {
        OnTourDeleteEvent?.Invoke(tour);
        Tours.Remove(tour);
    }
    
    private void EditTour(Tour tour)
    {
        int index = Tours.IndexOf(tour);
        Tours[index] = tour; 
    }
    
    private void OnSelectedTourChange() {
        OnSelectedTourChangedEvent?.Invoke(CurrentTour);
    }

    public void SetCurrentTour(Tour? tour) {
        CurrentTour = tour;
        OnSelectedTourChange();
    }
}