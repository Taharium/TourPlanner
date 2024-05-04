using BusinessLayer;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {

        private ObservableCollection<TourLogs> TourLogsObList = new();
        private IBusinessLogicTourLogs _businessLogicTourLogs;
        
        public TourLogsVM(IBusinessLogicTourLogs businessLogicTourLogs) {
            _businessLogicTourLogs = businessLogicTourLogs;
            TourLogsCollectionView ??= new(TourLogsObList);
            TourLogsCollectionView.Refresh();
            TourLogsCollectionView.MoveCurrentTo(null);

            AddTourLogCommand = new RelayCommand(OpenAddTourLog, CanExcuteAddTourLog);
            DeleteTourLogCommand = new RelayCommand(DeleteTourLog, CanExcuteDeleteEditTourLog);
            EditTourLogCommand = new RelayCommand(EditTourLog, CanExcuteDeleteEditTourLog);
        }

        private TourLogs? _selectedtourlog;
        public TourLogs? SelectedTourLog {
            get => _selectedtourlog;
            set {
                if (_selectedtourlog != value) {
                    _selectedtourlog = value;
                    OnPropertyChanged(nameof(SelectedTourLog));
                    //TourLogsCollectionView.Refresh();
                }
            }
        }

        private Tour? _selectedTour;
        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    if (_selectedTour != null) {
                        TourLogsObList = new(_selectedTour.TourLogsList);
                        TourLogsCollectionView.Refresh();
                    }
                    AddTourLogCommand.RaiseCanExecuteChanged();
                    OnPropertyChanged(nameof(SelectedTour));
                    //TourLogsCollectionView.Refresh();
                }
            }
        }



        public RelayCommand AddTourLogCommand { get; }
        public RelayCommand DeleteTourLogCommand { get; }
        public RelayCommand EditTourLogCommand { get; }

        public ListCollectionView TourLogsCollectionView { get; private set; }

        public EventHandler<TourLogs>? EditTourLogEvent;
        public EventHandler<Tour>? Update;

        

        public void OpenAddTourLog(object? a) {
            AddTourLogWindow addTourLogWindow = new();
            AddTourLogWindowVM addTourLogWindowVM = new AddTourLogWindowVM(addTourLogWindow);
            addTourLogWindow.DataContext = addTourLogWindowVM;
            addTourLogWindowVM.AddTourLogEvent += (s, e) => AddTourLog(e);
            addTourLogWindow.Show();
        }

        private void AddTourLog(TourLogs tourLogs) {
            if (SelectedTour != null) {
                _businessLogicTourLogs.AddTourLog(SelectedTour, tourLogs);
                TourLogsObList.Add(tourLogs);
                SelectedTourLog = tourLogs;
                TourLogsCollectionView.Refresh();
            }
        }

        public void EditTourLog(object? obj) {
            if (obj is TourLogs tourLogs) {
                EditTourLogWindow editTourLogWindow = new();
                EditTourLogWindowVM editTourLogWindowVM = new EditTourLogWindowVM(tourLogs, editTourLogWindow);
                editTourLogWindow.DataContext = editTourLogWindowVM;
                editTourLogWindowVM.EditTourLogEvent += (s, e) => EditTourLogFunction(e);
                editTourLogWindow.Show();
            }
        }

        private void EditTourLogFunction(TourLogs tourLogs) {
            if (SelectedTour != null) {
                _businessLogicTourLogs.UpdateTourLog(SelectedTour, tourLogs);
                int index = TourLogsObList.IndexOf(tourLogs);
                TourLogsObList[index] = tourLogs;
                SelectedTourLog = tourLogs;
                TourLogsCollectionView.Refresh();
            }
        }

        public bool CanExcuteDeleteEditTourLog(object? parameter) {
            return SelectedTourLog != null;
        }

        public bool CanExcuteAddTourLog(object? parameter) {
            return SelectedTour != null;
        }

        public void DeleteTourLog(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour log?", "Delete Tour Log", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is TourLogs tourlog && result == MessageBoxResult.Yes && SelectedTour != null) {
                _businessLogicTourLogs.DeleteTourLog(SelectedTour, tourlog);
                TourLogsObList.Remove(tourlog);
                TourLogsCollectionView.Refresh();
            }
        }

        public void SetTour(Tour? tour) {
            if (tour != null)
                SelectedTour = tour;
        }
    }
}
