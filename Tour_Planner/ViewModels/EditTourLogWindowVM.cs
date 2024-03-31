using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tour_Planner.Enums;

namespace Tour_Planner.ViewModels {
    public class EditTourLogWindowVM : ViewModelBase {
        private readonly Window _window;
        private TourLogs _tempTourLog;
        private TourLogs _tourLog;
        private Rating _selectedRating;
        private Difficulty _selectedDifficulty;


        public TourLogs TourLogs {
            get => _tempTourLog;
            set {
                if (_tempTourLog != value) {
                    _tempTourLog = value;
                    OnPropertyChanged(nameof(TourLogs));
                }
            }
        }

        public event EventHandler<TourLogs>? EditTourLogEvent;
        public RelayCommand FinishEditTourLogCommand { get; }

        public string DateTimeProp {
            get => _tempTourLog.DateTime.ToString();
            set {
                if (_tempTourLog.DateTime.ToString() != value) {
                    if (DateTime.TryParse(value, out DateTime parsedDate)) {
                        _tempTourLog.DateTime = parsedDate;
                    }
                    OnPropertyChanged(nameof(DateTimeProp));
                }
            }
        }

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

        public EditTourLogWindowVM(TourLogs tourLogs, Window window) {
            _errorMessage = "";
            _window = window;
            _tourLog = tourLogs;
            _tempTourLog = new TourLogs(tourLogs);
            FinishEditTourLogCommand = new RelayCommand((_) => EditTourLogsFunction());
        }

        private void UpdateTourLog() {
            _tourLog.DateTime = _tempTourLog.DateTime;
            _tourLog.TotalTime = _tempTourLog.TotalTime;
            _tourLog.Distance = _tempTourLog.Distance;
            _tourLog.Rating = _tempTourLog.Rating;
            _tourLog.Comment = _tempTourLog.Comment;
            _tourLog.Difficulty = _tempTourLog.Difficulty;
        }

        private void EditTourLogsFunction() {
            if (!IsTourLogValid()) {
                return;
            }
            ErrorMessage = "";
            UpdateTourLog();
            EditTourLogEvent?.Invoke(this, _tourLog);
            MessageBox.Show("Tour Log edited successfully!", "EditTourLog", MessageBoxButton.OK, MessageBoxImage.Information);
            _window.Close();
        }

        public bool IsTourLogValid() {

            if (_tempTourLog.DateTime > DateTime.Now) {
                ErrorMessage = "Please enter a valid Date and Time in the past";
                return false;
            }

            if (_tempTourLog.Distance == "") {
                ErrorMessage = "Please enter a Distance";
                return false;
            }

            if (int.TryParse(_tempTourLog.Distance, out int parsedDistance)) {
                _tempTourLog.Distance = parsedDistance.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Distance";
                return false;
            }

            if (_tempTourLog.TotalTime == "") {
                ErrorMessage = "Please enter a Time";
                return false;
            }

            if (int.TryParse(_tempTourLog.TotalTime, out int parsedTime)) {
                _tempTourLog.TotalTime = parsedTime.ToString();
            }
            else {
                ErrorMessage = "Please enter a valid number for Time";
                return false;
            }

            return true;

        }
    }
}
