using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private string _errorMessage;
        private Tour _tempTour;
        private Tour _tour;
        private string _selectedPlaceStart = "";
        private string _selectedPlaceEnd = "";
        private const int Threshold = 3;
        public ObservableCollection<string> SearchResultsStart { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> SearchResultsEnd { get; } = new ObservableCollection<string>();
        private readonly IWindowStore _windowStore;
        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenRouteService _openRouteService;

        private bool _isStartSearchTriggered;
        public bool IsStartSearchTriggered
        {
            get => _isStartSearchTriggered; 
            set {
                if (_isStartSearchTriggered != value) {
                    _isStartSearchTriggered = value;
                    OnPropertyChanged(nameof(IsStartSearchTriggered));
                }
            }
        }
        
        private bool _isEndSearchTriggered;
        public bool IsEndSearchTriggered
        {
            get => _isEndSearchTriggered; 
            set {
                if (_isEndSearchTriggered != value) {
                    _isEndSearchTriggered = value;
                    OnPropertyChanged(nameof(IsEndSearchTriggered));
                }
            }
        }
        
        private bool _shouldScroll;
        public bool ShouldScroll
        {
            get => _shouldScroll;
            set
            {
                if (_shouldScroll != value)
                {
                    _shouldScroll = value;
                    OnPropertyChanged(nameof(ShouldScroll));
                }
            }
        }
        
        public string SelectedPlaceStart {
            get => _selectedPlaceStart;
            set {
                if (_selectedPlaceStart != value) {
                    _selectedPlaceStart = value;
                    IsStartSearchTriggered = false;
                    OnPropertyChanged(nameof(SelectedPlaceStart));
                }
            }
        }
        
        public string SelectedPlaceEnd {
            get => _selectedPlaceEnd;
            set {
                if (_selectedPlaceEnd != value) {
                    _selectedPlaceEnd = value;
                    IsEndSearchTriggered = false;
                    OnPropertyChanged(nameof(SelectedPlaceEnd));
                }
            }
        }
        
        
        public string ErrorMessage {
            get => _errorMessage;
            set {
                if (_errorMessage != value) {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public Tour Tour {
            get => _tempTour;
            set {
                if (_tempTour != value) {
                    _tempTour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }

        public IEnumerable<TransportType> TransportTypes {
            get {
                return Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
            }
        }
        
        public RelayCommand FinishEditCommand { get; }
        
        public AsyncRelayCommand SearchPlaceStartCommand { get; }
        public AsyncRelayCommand SearchPlaceEndCommand { get; }

        public EditTourWindowVM(ITourStore tourStore, IWindowStore windowStore, IBusinessLogicTours businessLogicTours, 
            IMessageBoxService messageBoxService, IOpenRouteService openRouteService) {
            _openRouteService = openRouteService;
            _businessLogicTours = businessLogicTours;
            _messageBoxService = messageBoxService;
            _tour = tourStore.CurrentTour ?? new Tour();
            _tempTour = new Tour(_tour);
            SelectedPlaceEnd = _tempTour.EndLocation;
            SelectedPlaceStart = _tempTour.StartLocation;
            _errorMessage = "";
            _windowStore = windowStore;
            SearchPlaceStartCommand = new AsyncRelayCommand(searchplace => SearchStartLocationEdit(searchplace));
            SearchPlaceEndCommand = new AsyncRelayCommand(searchplace => SearchEndLocationEdit(searchplace));
            FinishEditCommand = new RelayCommand((_) => FinishEditFunction());
        }
        private async Task SearchStartLocationEdit(object? searchplace)
        {
            var searchplacestr = searchplace as string ?? "";
            
            var places = await _openRouteService.GetPlaces(searchplacestr);
            
            SearchResultsStart.Clear();
            foreach (var place in places)
            {
                SearchResultsStart.Add(place);
            }
            ShouldScroll = SearchResultsStart.Count >= Threshold;
            IsStartSearchTriggered = true;
        }

        private async Task SearchEndLocationEdit(object? searchplace)
        {
            var searchplacestr = searchplace as string ?? "";
            
            var places = await _openRouteService.GetPlaces(searchplacestr);
            
            SearchResultsEnd.Clear();
            foreach (var place in places)
            {
                SearchResultsEnd.Add(place);
            }
            ShouldScroll = SearchResultsEnd.Count >= Threshold;
            IsEndSearchTriggered = true;
        }


        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tempTour.Name) && !string.IsNullOrWhiteSpace(_tempTour.Description) &&
                   !string.IsNullOrWhiteSpace(_tempTour.StartLocation) && !string.IsNullOrWhiteSpace(_tempTour.EndLocation);
        }

        private void UpdateTour() {
            _tour.Name = _tempTour.Name;
            _tour.Description = _tempTour.Description;
            _tour.StartLocation = _selectedPlaceStart;
            _tour.EndLocation = _selectedPlaceEnd;
            _tour.Distance = _tempTour.Distance;
            _tour.TransportType = _tempTour.TransportType;
        }

        public void FinishEditFunction() {

            if (IsTourValid()) {
                try {
                    ErrorMessage = "";
                    UpdateTour();
                    _messageBoxService.Show("Tour edited successfully!", "EditTour", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _businessLogicTours.UpdateTour(_tour);
                    _windowStore.Close();
                }
                catch (BusinessLayerException e) {
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowStore.Close();
                }
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }

        }
    }
}
