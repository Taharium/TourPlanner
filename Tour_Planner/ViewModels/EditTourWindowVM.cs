using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private string _errorMessage;
        private Tour _tempTour;
        private Tour _tour;
        private Tour _tourBackUp;
        private string _selectedPlaceStart = "";
        private string _selectedPlaceEnd = "";
        private const int Threshold = 3;
        public ObservableCollection<string> SearchResultsStart { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> SearchResultsEnd { get; } = new ObservableCollection<string>();
        private readonly IWindowStore _windowStore;
        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenRouteService _openRouteService;

        //private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();

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
        
        public AsyncRelayCommand FinishEditCommand { get; }
        
        public AsyncRelayCommand SearchPlaceStartCommand { get; }
        public AsyncRelayCommand SearchPlaceEndCommand { get; }

        public EditTourWindowVM(ITourStore tourStore, IWindowStore windowStore, IBusinessLogicTours businessLogicTours, 
            IMessageBoxService messageBoxService, IOpenRouteService openRouteService) {
            _openRouteService = openRouteService;
            _businessLogicTours = businessLogicTours;
            _messageBoxService = messageBoxService;
            _tour = tourStore.CurrentTour ?? new Tour();
            _tempTour = new Tour(_tour);
            _tourBackUp = new Tour(_tour);
            SelectedPlaceEnd = _tempTour.EndLocation;
            SelectedPlaceStart = _tempTour.StartLocation;
            _errorMessage = "";
            _windowStore = windowStore;
            SearchPlaceStartCommand = new AsyncRelayCommand(searchplace => SearchStartLocationEdit(searchplace));
            SearchPlaceEndCommand = new AsyncRelayCommand(searchplace => SearchEndLocationEdit(searchplace));
            FinishEditCommand = new AsyncRelayCommand((_) => FinishEditFunction());
        }
        private async Task SearchStartLocationEdit(object? searchplace)
        {
            var searchplacestr = searchplace as string ?? "";
            
            List<string> places;
            try {
                places = await _openRouteService.GetPlaces(searchplacestr);
            }
            catch (BusinessLayerException e) {
                _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
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
            
            List<string> places;
            try {
                places = await _openRouteService.GetPlaces(searchplacestr);
            }
            catch (BusinessLayerException e) {
                _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
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

        private void BackUp() {
            _tour.Name = _tourBackUp.Name;
            _tour.Description = _tourBackUp.Description;
            _tour.StartLocation = _tourBackUp.StartLocation;
            _tour.EndLocation = _tourBackUp.EndLocation;
            _tour.Distance = _tourBackUp.Distance;
            _tour.TransportType = _tourBackUp.TransportType;
        }
        
        private void UpdateTour() {
            _tour.Name = _tempTour.Name;
            _tour.Description = _tempTour.Description;
            _tour.StartLocation = _selectedPlaceStart;
            _tour.EndLocation = _selectedPlaceEnd;
            _tour.Distance = _tempTour.Distance;
            _tour.TransportType = _tempTour.TransportType;
        }

        public async Task FinishEditFunction() {

            if (IsTourValid()) {
                try {
                    ErrorMessage = "";
                    UpdateTour();
                    await _businessLogicTours.UpdateTour(_tour);
                    _messageBoxService.Show("Tour edited successfully!", "EditTour", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    //Logger.Debug("Tour edited successfully!");
                    _windowStore.Close();
                }
                catch (BusinessLayerException e) {
                    BackUp();
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowStore.Close();
                    if (e.ErrorMessage.StartsWith("Database")) {
                        Environment.Exit(1); 
                    }
                }
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }
        }
    }
}
