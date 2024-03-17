using System;
using System.Windows;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class AddTourWindowVM : ViewModelBase {
        private Tour _tour;
        private string _errorMessage;
        public string ErrorMessage {
            get => _errorMessage;
            set {
                if (_errorMessage != value) {
                    _errorMessage = value;
                    RaisePropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        private readonly Window _window;

        public Tour Tour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    RaisePropertyChanged(nameof(Tour));
                }
            }
        }

        public event EventHandler<Tour>? AddTourEvent;
        public RelayCommand FinishAddCommand { get; }


        public AddTourWindowVM(Window window) {
            _errorMessage = "";
            _window = window;
            _tour = new Tour();
            FinishAddCommand = new RelayCommand((_) => AddFunction());
        }

        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tour.Name) && !string.IsNullOrWhiteSpace(_tour.Description) &&
                   !string.IsNullOrWhiteSpace(_tour.StartLocation) && !string.IsNullOrWhiteSpace(_tour.EndLocation) &&
                   !string.IsNullOrWhiteSpace(_tour.TransportType);
        }

        private void AddFunction() {        //could be used using constructor and ref Tour tour
            if (IsTourValid()) {
                ErrorMessage = "";
                AddTourEvent?.Invoke(this, _tour);
                MessageBox.Show("Tour added successfully!", "AddTour", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }
        }

    }
}
