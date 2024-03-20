using System.Windows;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class EditTourWindowVM : ViewModelBase {

        private readonly Window _window;
        private string _errorMessage;
        private Tour _tourTemp;
        private Tour _tour;

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

        //public event EventHandler<Tour>? EditTourEvent;

        public RelayCommand FinishEditCommand { get; }

        public EditTourWindowVM(ref Tour tour, Window window) {
            _tour = tour;
            _tourTemp = new Tour(tour);
            _errorMessage = "";
            _window = window;
            FinishEditCommand = new RelayCommand((_) => FinishEditFunction());
        }

        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tourTemp.Name) && !string.IsNullOrWhiteSpace(_tourTemp.Description) &&
                   !string.IsNullOrWhiteSpace(_tourTemp.StartLocation) && !string.IsNullOrWhiteSpace(_tourTemp.EndLocation);
        }


        private void UpdateTour() {
            _tour.Name = _tourTemp.Name;
            _tour.Description = _tourTemp.Description;
            _tour.StartLocation = _tourTemp.StartLocation;
            _tour.EndLocation = _tourTemp.EndLocation;
            _tour.TransportType = _tourTemp.TransportType;
        }


        public void FinishEditFunction() {

            if (IsTourValid()) {
                ErrorMessage = "";
                UpdateTour();
                //EditTourEvent?.Invoke(this, _tour);
                MessageBox.Show("Tour edited successfully!", "EditTour", MessageBoxButton.OK, MessageBoxImage.Information);
                _window.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }

        }
    }
}
