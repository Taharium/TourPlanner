using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BusinessLayer;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private string _errorMessage;
        private Tour _tempTour;
        private Tour _tour;
        private readonly IWindowStore _windowStore;
        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IMessageBoxService _messageBoxService;

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

        public event EventHandler<Tour>? EditTourEvent;

        public RelayCommand FinishEditCommand { get; }

        public EditTourWindowVM(ITourStore tourStore, IWindowStore windowStore, IBusinessLogicTours businessLogicTours, IMessageBoxService messageBoxService) {
            _businessLogicTours = businessLogicTours;
            _messageBoxService = messageBoxService;
            _tour = tourStore.CurrentTour ?? new Tour();
            _tempTour = new Tour(_tour);
            _errorMessage = "";
            // _window = window;
            _windowStore = windowStore;
            FinishEditCommand = new RelayCommand((_) => FinishEditFunction());
        }

        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tempTour.Name) && !string.IsNullOrWhiteSpace(_tempTour.Description) &&
                   !string.IsNullOrWhiteSpace(_tempTour.StartLocation) && !string.IsNullOrWhiteSpace(_tempTour.EndLocation);
        }

        private void UpdateTour() {
            _tour.Name = _tempTour.Name;
            _tour.Description = _tempTour.Description;
            _tour.StartLocation = _tempTour.StartLocation;
            _tour.EndLocation = _tempTour.EndLocation;
            _tour.Distance = _tempTour.Distance;
            _tour.TransportType = _tempTour.TransportType;
        }

        public void FinishEditFunction() {

            if (IsTourValid()) {
                ErrorMessage = "";
                UpdateTour();
                _messageBoxService.Show("Tour edited successfully!", "EditTour", MessageBoxButton.OK, MessageBoxImage.Information);
                //EditTourEvent?.Invoke(this, _tour);
                _businessLogicTours.UpdateTour(_tour);
                _windowStore.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }

        }
    }
}
