using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BusinessLayer;
using BusinessLayer.BLException;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;

namespace Tour_Planner.ViewModels {

    public class TabControlVM : ViewModelBase {
        private int _selectedTab;
        private Tour? _tour;
        private Joke? _joke;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IOpenRouteService _openRouteService;
        private readonly IOpenWeatherService _openWeatherService;
        private readonly IGetJokeService _getJokeService;
        private ObservableCollection<Weather> _weatherList = new ObservableCollection<Weather>();

        public event Action? UpdatedRoute;
        private string _buttonText = "Select a Tour first";

        public Joke? Joke {
            get => _joke;
            set {
                if (_joke != value) {
                    _joke = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public string ButtonText {
            get => _buttonText;
            set {
                if (_buttonText != value) {
                    _buttonText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Weather> WeatherList {
            get => _weatherList;
            set {
                if (_weatherList != value) {
                    _weatherList = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public Tour? SelectedTour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                    UpdateButtonText();
                }
            }
        }
        
        private void UpdateButtonText() {
            if (SelectedTour != null) {
                ButtonText = $"Get Weather for {SelectedTour.EndLocation}: ";
            }
            else {
                ButtonText = "Select a Tour first";
            }
        }

        public int SelectedTab {
            get => _selectedTab;
            set {
                if (_selectedTab != value) {
                    _selectedTab = value;
                    OnPropertyChanged(nameof(SelectedTab));
                }
            }
        }
        
        public AsyncRelayCommand GetEndWeatherCommand { get; }
        public AsyncRelayCommand GetJokeCommand { get; }

        public TabControlVM(ITourStore tourStore, IMessageBoxService messageBoxService, IOpenWeatherService openWeatherService,
            IOpenRouteService openRouteService, IGetJokeService getJokeService) {
            _getJokeService = getJokeService;
            _openRouteService = openRouteService;
            _openWeatherService = openWeatherService;
            _messageBoxService = messageBoxService;
            tourStore.OnSelectedTourChangedEvent += SetTour;
            tourStore.OnTourDeleteEvent += ClearTour;
            tourStore.OnSelectedTourChangedEvent += GenerateRoute;
            _tour = tourStore.CurrentTour;
            _selectedTab = (int)TabControlEnum.General;

            GetEndWeatherCommand = new AsyncRelayCommand((_) => GetEndWeatherFunction());
            GetJokeCommand = new AsyncRelayCommand((_) => GetJokeFunction());
        }

        private async Task GetJokeFunction() {
            try {
                Joke = await _getJokeService.GetJoke();
            }
            catch (BusinessLayerException e) {
                _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GetEndWeatherFunction() {
            WeatherList.Clear();
            if(SelectedTour == null) return;
            
            try {
                var coordinates = await _openRouteService.GetGeoCoordinates(SelectedTour.EndLocation);
                var weathers = await _openWeatherService.GetWeather(coordinates);
                foreach (var weather in weathers) {
                    WeatherList.Add(weather);
                }
            }
            catch (BusinessLayerException e) {
                _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearTour(Tour? tour)
        {
            if (tour != null && tour == _tour)
            {
                SelectedTour = null;
            }
        }

        private void SetTour(Tour? tour) {
            if (tour != null) {
                if (SelectedTab != (int)TabControlEnum.Joke) { 
                    SelectedTab = (int)TabControlEnum.General;  
                }
                SelectedTour = tour;
                WeatherList.Clear();
            }
        }

        private void GenerateRoute(Tour? tour) {
            string direction = tour?.Directions ?? "about:blank";
            //TODO: Logging
            try {
                string directionsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets/Resource/directions.js");
                string directions = $"let directions = {direction};";
                File.WriteAllText(directionsFile, directions);
                UpdatedRoute?.Invoke();
            }
            catch (Exception) {
                _messageBoxService.Show("Failed to write into direction.js!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
