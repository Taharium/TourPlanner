using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {
        private TourLogs _tourLogs;
        public TourLogs TourLogs {
            get => _tourLogs;
            set {
                if (_tourLogs != value) {
                    _tourLogs = value;
                    RaisePropertyChanged(nameof(TourLogs));
                }
            }
        }
        public TourLogsVM() {
            _tourLogs = new TourLogs();
        }
    }
}
