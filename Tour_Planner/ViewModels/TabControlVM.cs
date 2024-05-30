using System;
using System.IO;
using System.Windows;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;

namespace Tour_Planner.ViewModels {

    public class TabControlVM : ViewModelBase {
        private int _selectedTab;
        private Tour? _tour;
        private readonly IMessageBoxService _messageBoxService;

        public event Action? UpdatedRoute;
        
        public Tour? SelectedTour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
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

        public TabControlVM(ITourStore tourStore, IMessageBoxService messageBoxService) {
            _messageBoxService = messageBoxService;
            tourStore.OnSelectedTourChangedEvent += SetTour;
            tourStore.OnTourDeleteEvent += ClearTour;
            tourStore.OnSelectedTourChangedEvent += GenerateRoute;
            _tour = tourStore.CurrentTour;
            _selectedTab = (int)TabControlEnum.General;
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
                SelectedTab = (int)TabControlEnum.General;
                SelectedTour = tour;
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
