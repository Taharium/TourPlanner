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
using Tour_Planner.Logging;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class AddTourWindowVM : ViewModelBase {
        private Tour _tour;
        private string _errorMessage;
        private string _selectedPlaceStart = "";
        private string _selectedPlaceEnd = "";
        private const int Threshold = 3;
        public ObservableCollection<string> SearchResultsStart { get; } = new ObservableCollection<string>();
        public ObservableCollection<string> SearchResultsEnd { get; } = new ObservableCollection<string>();

        private TransportType _selectedTransportType;
        private readonly IWindowStore _windowStore;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IOpenRouteService _openRouteService;
        private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();

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
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }

        public TransportType SelectedRating {
            get { return _selectedTransportType; }
            set {
                if (_selectedTransportType != value) {
                    _selectedTransportType = value;
                    OnPropertyChanged(nameof(SelectedRating));
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
        

        public IEnumerable<TransportType> TransportTypes {
            get {
                return Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
            }
        }

        public AsyncRelayCommand FinishAddCommand { get; }
        
        public AsyncRelayCommand SearchPlaceStartCommand { get; }
        public AsyncRelayCommand SearchPlaceEndCommand { get; }



        public AddTourWindowVM(IWindowStore windowStore, IMessageBoxService messageBoxService, 
            IBusinessLogicTours businessLogicTour, IOpenRouteService openRouteService){
            _errorMessage = "";
            _windowStore = windowStore;
            _messageBoxService = messageBoxService;
            _businessLogicTours = businessLogicTour;
            _openRouteService = openRouteService;
            _tour = new Tour();
            SearchPlaceStartCommand = new AsyncRelayCommand((searchplace) => SearchPlaceStart(searchplace));
            SearchPlaceEndCommand = new AsyncRelayCommand((searchplace) => SearchPlaceEnd(searchplace));
            FinishAddCommand = new AsyncRelayCommand((_) => AddFunction());
        }

        public async Task SearchPlaceStart(object? searchplace)
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
        
        public async Task SearchPlaceEnd(object? searchplace)
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
            return !string.IsNullOrWhiteSpace(_tour.Name) && !string.IsNullOrWhiteSpace(_tour.Description) &&
                   !string.IsNullOrWhiteSpace(_tour.StartLocation) && !string.IsNullOrWhiteSpace(_tour.EndLocation);
        }

        public async Task AddFunction() {
            _tour.StartLocation = _selectedPlaceStart;
            _tour.EndLocation = _selectedPlaceEnd;
            if (IsTourValid()) {
                try {
                    ErrorMessage = "";
                    await _businessLogicTours.AddTour(_tour);
                    _messageBoxService.Show("Tour added successfully!", "AddTour", MessageBoxButton.OK, MessageBoxImage.Information);
                    Logger.Debug("Tour added successfully!");
                    _windowStore.Close();
                }
                catch (BusinessLayerException e) {
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //Logger.Error(e.ErrorMessage);
                    _windowStore.Close();
                }
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }
        }

    }
}
