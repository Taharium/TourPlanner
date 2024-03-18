using System;
using System.Collections.ObjectModel;
using Tour_Planner.Models;
using Tour_Planner.Enums;
using Tour_Planner.WindowsWPF;
using System.Windows;
using System.Diagnostics;
using System.Windows.Data;

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

        private TourLogs _selectedtourlog = new();
        public TourLogs SelectedTourLog {
            get => _selectedtourlog;
            set {
                if (_selectedtourlog != value) {
                    _selectedtourlog = value;
                    RaisePropertyChanged(nameof(SelectedTourLog));
                    EditTourLogEvent?.Invoke(this, _selectedtourlog);
                }
            }
        }


        public RelayCommand AddTourLogCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand EditTourLogCommand { get; }


        private ListCollectionView? _tourlogscollectionview;

        public ListCollectionView TourLogsCollectionView {
            get {
                _tourlogscollectionview ??= new(TourLogsList);
                _tourlogscollectionview.Refresh();
                _tourlogscollectionview.MoveCurrentTo(null);
                return _tourlogscollectionview;
            }
        }

        public EventHandler<TourLogs>? EditTourLogEvent;

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

        public void DeleteTourLog(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour log?", "Delete Tour Log", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is TourLogs tourlog && result == MessageBoxResult.Yes) {
                TourLogsList.Remove(tourlog);
            }
        }

    }
}
