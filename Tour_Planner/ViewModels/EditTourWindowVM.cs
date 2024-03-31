using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tour_Planner.Enums;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private readonly Window _window;
        private string _errorMessage;
        private Tour _tempTour;
        private Tour _tour;

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

        public EditTourWindowVM(Tour tour, Window window) {
            _tour = tour;
            _tempTour = new Tour(tour);
            _errorMessage = "";
            _window = window;
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
                MessageBox.Show("Tour edited successfully!", "EditTour", MessageBoxButton.OK, MessageBoxImage.Information);
                EditTourEvent?.Invoke(this, _tour);
                _window.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }

        }
    }
}
