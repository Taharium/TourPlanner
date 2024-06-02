using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using DataAccessLayer.Logging;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class EditTourLogWindowVM : ViewModelBase {
        private TourLogs _tempTourLog;
        private TourLogs _tourLog;
        private TourLogs _tourLogBackUp;
        private Rating _selectedRating;
        private Difficulty _selectedDifficulty;
        private readonly IWindowStore _windowStore;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IBusinessLogicTourLogs _businessLogicTourLogs;
        private readonly Tour? _tour;
        private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();

        public TourLogs TourLogs {
            get => _tempTourLog;
            set {
                if (_tempTourLog != value) {
                    _tempTourLog = value;
                    OnPropertyChanged(nameof(TourLogs));
                }
            }
        }

        public AsyncRelayCommand FinishEditTourLogCommand { get; }

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

        public EditTourLogWindowVM(IWindowStore windowStore, ITourStore tourStore, ITourLogStore tourLogStore, 
            IBusinessLogicTourLogs businessLogicTourLogs, IMessageBoxService messageBoxService) {
            _tour = tourStore.CurrentTour;
            _windowStore = windowStore;
            _businessLogicTourLogs = businessLogicTourLogs;
            _messageBoxService = messageBoxService;
            _tourLog = tourLogStore.CurrentTour ?? new TourLogs();
            _tempTourLog = new TourLogs(_tourLog);
            _tourLogBackUp = new TourLogs(_tourLog);
            _errorMessage = "";
            FinishEditTourLogCommand = new AsyncRelayCommand((_) => EditTourLogsFunction());
        }

        private void BackUp() {
            _tourLog.DateTime = _tourLogBackUp.DateTime;
            _tourLog.TotalTime = _tourLogBackUp.TotalTime;
            _tourLog.Distance = _tourLogBackUp.Distance;
            _tourLog.Rating = _tourLogBackUp.Rating;
            _tourLog.Comment = _tourLogBackUp.Comment;
            _tourLog.Difficulty = _tourLogBackUp.Difficulty;
        }
        
        private void UpdateTourLog() {
            _tourLog.DateTime = _tempTourLog.DateTime;
            _tourLog.TotalTime = _tempTourLog.TotalTime;
            _tourLog.Distance = _tempTourLog.Distance;
            _tourLog.Rating = _tempTourLog.Rating;
            _tourLog.Comment = _tempTourLog.Comment;
            _tourLog.Difficulty = _tempTourLog.Difficulty;
        }

        private async Task EditTourLogsFunction() {
            if (!IsTourLogValid()) {
                return;
            }
            ErrorMessage = "";
            UpdateTourLog();
            if (_tour != null) {
                try {
                    await _businessLogicTourLogs.UpdateTourLog(_tour, _tourLog);
                    _messageBoxService.Show("Tour Log edited successfully!", "EditTourLog", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    _windowStore.Close();
                }
                catch (BusinessLayerException e) {
                    BackUp();
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _windowStore.Close();
                    if (e.ErrorMessage.StartsWith("Database")) {
                        Environment.Exit(1); 
                    }
                }
            }
        }

        public bool IsTourLogValid() {

            if (_tempTourLog.DateTime > DateTime.Now) {
                ErrorMessage = "Please enter a valid Date and Time in the past!";
                return false;
            }

            if (_tempTourLog.Distance == "") {
                ErrorMessage = "Please enter a Distance!";
                return false;
            }

            if (int.TryParse(_tempTourLog.Distance, out int parsedDistance)) {
                _tempTourLog.Distance = parsedDistance.ToString();
            }
            else {
                Logger.Warn("User did not enter a valid number for Distance");
                ErrorMessage = "Please enter a valid number for Distance!";
                return false;
            }

            if (_tempTourLog.TotalTime == "") {
                ErrorMessage = "Please enter a Time!";
                return false;
            }

            if (int.TryParse(_tempTourLog.TotalTime, out int parsedTime)) {
                _tempTourLog.TotalTime = parsedTime.ToString();
            }
            else {
                Logger.Warn("User did not enter a valid number for Time");
                ErrorMessage = "Please enter a valid number for Time!";
                return false;
            }

            if (_tempTourLog.DateTime.Kind != DateTimeKind.Utc) {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
                DateTime utcDateTime = TimeZoneInfo.ConvertTimeToUtc(_tempTourLog.DateTime, localTimeZone);
                _tempTourLog.DateTime = utcDateTime;
            }

            return true;

        }
    }
}
