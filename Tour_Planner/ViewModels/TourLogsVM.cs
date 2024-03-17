using System;
using System.Collections.ObjectModel;
using Tour_Planner.Models;
using Tour_Planner.Enums;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {
        public ObservableCollection<TourLogs> TourLogsList { get; set; } = [
            new TourLogs() {
                DateTime = new DateTime(2024, 3, 16),
                TotalTime = "2",
                Distance = "100",
                Rating = Rating.VeryGood,
                Comment = "Good",
                Difficulty = Difficulty.Easy
            },
            new TourLogs() {
                DateTime = new DateTime(2024, 3, 13),
                TotalTime = "5",
                Distance = "10000",
                Rating = Rating.VeryBad,
                Comment = "Never again",
                Difficulty = Difficulty.Hard
            },
            ];

        public RelayCommand AddTourLogCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand EditTourLogCommand { get; }

        public TourLogsVM() {
            AddTourLogCommand = new RelayCommand((_) => OpenAddTourLog());
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog);
            EditTourLogCommand = new RelayCommand(EditTourLog);
        }
        public void OpenAddTourLog() {
            AddTourLogWindow addTourLogWindow = new AddTourLogWindow();
            addTourLogWindow.Show();
        }

        public void EditTourLog(object? obj) {
            throw new NotImplementedException();
        }

        public void DeleteTourLog(object? obj) {
            throw new NotImplementedException();
        }

    }
}
