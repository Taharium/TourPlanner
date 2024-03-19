using System;
using System.Diagnostics;
using System.Windows;
using Tour_Planner.Models;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {

        private TourLogs? _selectedtourlog;
        public TourLogs? SelectedTourLog {
            get => _selectedtourlog;
            set {
                if (_selectedtourlog != value) {
                    _selectedtourlog = value;
                    RaisePropertyChanged(nameof(SelectedTourLog));
                }
            }
        }

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    RaisePropertyChanged(nameof(SelectedTour));
                    AddTourLogCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public RelayCommand AddTourLogCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand EditTourLogCommand { get; }


        /*private ListCollectionView? _tourlogscollectionview;

        public ListCollectionView TourLogsCollectionView {
            get {
                _tourlogscollectionview ??= new(TourLogsList);
                _tourlogscollectionview.Refresh();
                _tourlogscollectionview.MoveCurrentTo(null);
                return _tourlogscollectionview;
            }
        }*/

        public EventHandler<TourLogs>? EditTourLogEvent;

        public TourLogsVM() {
            AddTourLogCommand = new RelayCommand(OpenAddTourLog, CanExcuteAddTourLog);
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog, CanExcuteDeleteEditTourLog);
            EditTourLogCommand = new RelayCommand(EditTourLog, CanExcuteDeleteEditTourLog);
        }

        public void OpenAddTourLog(object? a) {
            AddTourLogWindow addTourLogWindow = new();
            AddTourLogWindowVM addTourLogWindowVM = new AddTourLogWindowVM(addTourLogWindow);
            addTourLogWindow.DataContext = addTourLogWindowVM;
            addTourLogWindowVM.AddTourLogEvent += (s, e) => AddTourLog(e);
            addTourLogWindow.Show();
        }

        private void AddTourLog(TourLogs tourLogs) {
            _selectedTour?.TourLogsList.Add(tourLogs);
            SelectedTourLog = tourLogs;
        }

        public void EditTourLog(object? obj) {
            throw new NotImplementedException();
        }

        public bool CanExcuteDeleteEditTourLog(object? parameter) {
            return SelectedTourLog != null;
        }

        public bool CanExcuteAddTourLog(object? parameter) {
            Debug.WriteLine(SelectedTour);
            return SelectedTour != null;
        }

        public void DeleteTourLog(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour log?", "Delete Tour Log", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is TourLogs tourlog && result == MessageBoxResult.Yes) {
                _selectedTour?.TourLogsList.Remove(tourlog);
            }
        }

        public void SetTour(Tour? tour) {
            if (tour != null)
                SelectedTour = tour;
        }
    }
}
