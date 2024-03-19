using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tour_Planner.Enums;
using Tour_Planner.Models;

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
                    RaisePropertyChanged(nameof(TourLogs));
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
                    RaisePropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public Rating SelectedRating {
            get { return _selectedRating; }
            set {
                if (_selectedRating != value) {
                    _selectedRating = value;
                    RaisePropertyChanged(nameof(SelectedRating));
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
                    RaisePropertyChanged(nameof(SelectedDifficulty));
                }
            }
        }

        public IEnumerable<Difficulty> DifficultyOptions {
            get {
                return Enum.GetValues(typeof(Difficulty)).Cast<Difficulty>();
            }
        }

        public AddTourLogWindowVM(Window window) {
            _errorMessage = "";
            _window = window;
            _tourLog = new TourLogs();
            FinishAddTourLogCommand = new RelayCommand((_) => AddTourLogFunction());
        }

        private void AddTourLogFunction() {
            if (!IsTourLogValid()) {
                return;
            }

            ErrorMessage = "";
            AddTourLogEvent?.Invoke(this, _tourLog);
            MessageBox.Show("Tour Log added successfully!", "AddTourLog", MessageBoxButton.OK, MessageBoxImage.Information);
            _window.Close();
        }

        private bool IsTourLogValid() {

            if (_tourLog.DateTime > DateTime.Now || !_tourLog.DateTime.HasValue) {
                ErrorMessage = "Please enter a valid Date";
                return false;
            }

            if (_tourLog.TotalTime == "") {
                ErrorMessage = "Please enter a Time";
                return false;
            }

            if (_tourLog.Distance == "") {
                ErrorMessage = "Please enter a Distance";
                return false;
            }

            if (int.TryParse(_tourLog.TotalTime, out int parsedTime)) {
                _tourLog.TotalTime = parsedTime.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Time";
                return false;
            }

            if (int.TryParse(_tourLog.Distance, out int parsedDistance)) {
                _tourLog.Distance = parsedDistance.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Distance";
                return false;
            }

            return true;

        }
    }
}
