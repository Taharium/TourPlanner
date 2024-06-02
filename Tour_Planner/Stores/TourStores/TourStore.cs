using System;
using System.Collections.ObjectModel;
using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using Models;
using Tour_Planner.Services.MessageBoxServices;

namespace Tour_Planner.Stores.TourStores;

public class TourStore : ITourStore {
    public Tour? CurrentTour { get; private set; }

    public ObservableCollection<Tour> Tours { get; set; } = new();

    public event Action<Tour?>? OnSelectedTourChangedEvent;

    public event Action<Tour?>? OnTourDeleteEvent;
    public event Action<Tour?>? OnTourAddedEvent;

    private readonly IBusinessLogicTours _businessLogicTours;
    private readonly IMessageBoxService _messageBoxService;

    public TourStore(IBusinessLogicTours businessLogicTours, IMessageBoxService messageBoxService) {
        _businessLogicTours = businessLogicTours;
        _messageBoxService = messageBoxService;
        LoadTours();

        _businessLogicTours.AddTourEvent += AddTour;
        _businessLogicTours.OnTourDeleteEvent += DeleteTour;
        _businessLogicTours.OnTourUpdateEvent += EditTour;
    }

    private async void LoadTours() {
        try {
            var tours = await _businessLogicTours.GetTours();

            foreach (var tour in tours) {
                Tours.Add(tour);
            }
        }
        catch (BusinessLayerException e) {
            _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            if (e.ErrorMessage.StartsWith("Database")) {
                Environment.Exit(1); 
            }
        }
    }

    private void AddTour(Tour tour) {
        //LoadTours();
        Tours.Add(tour);
        OnTourAddedEvent?.Invoke(tour);
    }

    private void DeleteTour(Tour tour) {
        OnTourDeleteEvent?.Invoke(tour);
        Tours.Remove(tour);
    }

    private void EditTour(Tour tour) {
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