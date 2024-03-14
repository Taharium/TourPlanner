using System.Windows;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private Tour _tour;
        private Tour _tourTemp;
        private readonly Window _window;
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

        public Tour Tour {
            get => _tourTemp;
            set {
                if (_tourTemp != value) {
                    _tourTemp = value;
                    RaisePropertyChanged(nameof(Tour));
                }
            }
        }

        public RelayCommand FinishEditCommand { get; }

        public EditTourWindowVM(ref Tour tour, Window window) {
            _tour = tour;
            _tourTemp = new Tour {
                Name = tour.Name,
                Description = tour.Description,
                StartLocation = tour.StartLocation,
                EndLocation = tour.EndLocation,
                TransportType = tour.TransportType
            };
            _errorMessage = "";
            _window = window;
            FinishEditCommand = new RelayCommand((_) => FinishEditFunction());
        }

        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tourTemp.Name) && !string.IsNullOrWhiteSpace(_tourTemp.Description) &&
                   !string.IsNullOrWhiteSpace(_tourTemp.StartLocation) && !string.IsNullOrWhiteSpace(_tourTemp.EndLocation) &&
                   !string.IsNullOrWhiteSpace(_tourTemp.TransportType);
        }

        private void FinishEditFunction() {
            if (IsTourValid()) {
                ErrorMessage = "";
                _tour.Name = _tourTemp.Name;
                _tour.Description = _tourTemp.Description;
                _tour.StartLocation = _tourTemp.StartLocation;
                _tour.EndLocation = _tourTemp.EndLocation;
                _tour.TransportType = _tourTemp.TransportType;
                _window.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }
        }
    }
}
