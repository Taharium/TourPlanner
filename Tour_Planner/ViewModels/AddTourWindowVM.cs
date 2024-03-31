using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tour_Planner.Enums;

namespace Tour_Planner.ViewModels {
    public class AddTourWindowVM : ViewModelBase {
        private Tour _tour;
        private string _errorMessage;
        private TransportType _selectedTransportType;

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

        public TransportType SelectedRating {
            get { return _selectedTransportType; }
            set {
                if (_selectedTransportType != value) {
                    _selectedTransportType = value;
                    RaisePropertyChanged(nameof(SelectedRating));
                }
            }
        }

        public IEnumerable<TransportType> TransportTypes {
            get {
                return Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
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
                   !string.IsNullOrWhiteSpace(_tour.StartLocation) && !string.IsNullOrWhiteSpace(_tour.EndLocation);
        }

        public void AddFunction() {        //could be used using constructor and ref Tour tour
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
