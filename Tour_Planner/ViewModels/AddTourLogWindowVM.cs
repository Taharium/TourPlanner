using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tour_Planner.Enums;

namespace Tour_Planner.ViewModels {
    public class AddTourLogWindowVM : ViewModelBase {

        private readonly Window _window;
        private TourLogs _tourLog;
        private Rating _selectedRating;
        private Difficulty _selectedDifficulty;

        public TourLogs TourLogs {
            get => _tourLog;
            set {
                if (_tourLog != value) {
                    _tourLog = value;
                    OnPropertyChanged(nameof(TourLogs));
                }
            }
        }

        public event EventHandler<TourLogs>? AddTourLogEvent;
        public RelayCommand FinishAddTourLogCommand { get; }

        private string _errorMessage;
        public string ErrorMessage {
            get => _errorMessage;
            set {
                if (_errorMessage != value) {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public Rating SelectedRating {
            get { return _selectedRating; }
            set {
                if (_selectedRating != value) {
                    _selectedRating = value;
                    OnPropertyChanged(nameof(SelectedRating));
                }
            }
        }

        public IEnumerable<Rating> RatingOptions {
            get {
                return Enum.GetValues(typeof(Rating)).Cast<Rating>();
            }
        }

        public Difficulty SelectedDifficulty {
            get { return _selectedDifficulty; }
            set {
                if (_selectedDifficulty != value) {
                    _selectedDifficulty = value;
                    OnPropertyChanged(nameof(SelectedDifficulty));
                }
            }
        }

        public IEnumerable<Difficulty> DifficultyOptions {
            get {
                return Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
            }
        }

        public string DateTimeProp {
            get => _tourLog.DateTime.ToString();
            set {
                if (_tourLog.DateTime.ToString() != value) {
                    if (DateTime.TryParse(value, out DateTime parsedDate)) {
                        _tourLog.DateTime = parsedDate;
                    }
                    OnPropertyChanged(nameof(DateTimeProp));
                }
            }
        }

        public AddTourLogWindowVM(Window window) {
            _errorMessage = "";
            _window = window;
            _tourLog = new TourLogs();
            FinishAddTourLogCommand = new RelayCommand((_) => AddTourLogFunction());
        }

        public void AddTourLogFunction() {
            if (!IsTourLogValid()) {
                return;
            }

            ErrorMessage = "";
            AddTourLogEvent?.Invoke(this, _tourLog);
            MessageBox.Show("Tour Log added successfully!", "AddTourLog", MessageBoxButton.OK, MessageBoxImage.Information);
            _window.Close();
        }

        private bool IsTourLogValid() {

            if (_tourLog.DateTime > DateTime.Now) {
                ErrorMessage = "Please enter a valid Date and Time in the past";
                return false;
            }

            if (_tourLog.Distance == "") {
                ErrorMessage = "Please enter a Distance";
                return false;
            }

            if (_tourLog.TotalTime == "") {
                ErrorMessage = "Please enter a Time";
                return false;
            }

            if (int.TryParse(_tourLog.Distance, out int parsedDistance)) {
                _tourLog.Distance = parsedDistance.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Distance";
                return false;
            }

            if (int.TryParse(_tourLog.TotalTime, out int parsedTime)) {
                _tourLog.TotalTime = parsedTime.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Time";
                return false;
            }

            return true;
        }
    }
}
