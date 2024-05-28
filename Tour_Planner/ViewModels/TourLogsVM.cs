﻿using BusinessLayer;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourLogsVM : ViewModelBase {

        private ObservableCollection<TourLogs> TourLogsObList = [
            new TourLogs() {
                Comment = "He",
                Distance = "2",
                TotalTime = "2"
            },
            new TourLogs() {
                Comment = "je",
                Distance = "3",
                TotalTime = "3"
            }
        ];
        private readonly IBusinessLogicTourLogs _businessLogicTourLogs;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ITourLogStore _tourLogStore;
        private readonly IWindowService<EditTourLogWindowVM, EditTourLogWindow> _editTourLogWindow;
        private readonly IWindowService<AddTourLogWindowVM, AddTourLogWindow> _addTourLogWindow;

        public TourLogsVM(IBusinessLogicTourLogs businessLogicTourLogs,ITourLogStore tourLogStore, ITourStore tourStore, IMessageBoxService messageBoxService, 
                        IWindowService<EditTourLogWindowVM, EditTourLogWindow> editTourLogWindow,
                        IWindowService<AddTourLogWindowVM, AddTourLogWindow> addTourLogWindow) {
            _editTourLogWindow = editTourLogWindow;
            _addTourLogWindow = addTourLogWindow;
            _messageBoxService = messageBoxService;
            tourStore.OnSelectedTourChangedEvent += SetTour;
            _tourLogStore = tourLogStore;
            _selectedTour = tourStore.CurrentTour;
            _businessLogicTourLogs = businessLogicTourLogs;
            _businessLogicTourLogs.OnTourLogDeleteEvent += DeleteTourLog;
            _businessLogicTourLogs.OnTourLogUpdateEvent += EditTourLog;
            _businessLogicTourLogs.AddTourLogEvent += AddTourLog;
            TourLogsCollectionView ??= new(TourLogsObList);
            TourLogsCollectionView.Refresh();
            TourLogsCollectionView.MoveCurrentTo(null);

            AddTourLogCommand = new RelayCommand((_) => OpenAddTourLog(), (_) => CanExecuteAddTourLog());
            DeleteTourLogCommand = new RelayCommand((_) => OnDeleteTourLog(), (_) => CanExecuteDeleteEditTourLog());
            EditTourLogCommand = new RelayCommand((_) => OpenEditTourLog(), (_) => CanExecuteDeleteEditTourLog());
        }

        private TourLogs? _selectedTourLog;
        public TourLogs? SelectedTourLog {
            get => _selectedTourLog;
            set {
                if (_selectedTourLog != value) {
                    _selectedTourLog = value;
                    OnPropertyChanged(nameof(SelectedTourLog));
                    _tourLogStore.SetCurrentTour(SelectedTourLog);
                    EditTourLogCommand.RaiseCanExecuteChanged();
                    DeleteTourLogCommand.RaiseCanExecuteChanged();
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
                    Debug.WriteLine($"TourLogsVM: {SelectedTour?.Name} {SelectedTour?.Id}");
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



        private void OpenAddTourLog() {
            _addTourLogWindow.ShowDialog();
        }

        private void AddTourLog(TourLogs tourLogs) {
            TourLogsObList.Add(tourLogs);
            SelectedTourLog = tourLogs;
            TourLogsCollectionView.Refresh();
        }

        private void OpenEditTourLog() { 
            _editTourLogWindow.ShowDialog();
        }

        private void EditTourLog(TourLogs tourLogs) {
            //_businessLogicTourLogs.UpdateTourLog(SelectedTour, tourLogs);
            int index = TourLogsObList.IndexOf(tourLogs);
            TourLogsObList[index] = tourLogs;
            SelectedTourLog = tourLogs;
            TourLogsCollectionView.Refresh();
        }

        private bool CanExecuteDeleteEditTourLog() {
            return SelectedTourLog != null;
        }

        private bool CanExecuteAddTourLog() {
            return SelectedTour != null;
        }

        private void OnDeleteTourLog() {
            MessageBoxResult result = _messageBoxService.Show("Are you sure you want to delete this tour log?", "Delete Tour Log", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (SelectedTourLog != null && result == MessageBoxResult.Yes && SelectedTour != null) {
                _businessLogicTourLogs.DeleteTourLog(SelectedTour, SelectedTourLog);
            }
        }

        private void DeleteTourLog(TourLogs tourLogs) {
            TourLogsObList.Remove(tourLogs);
            TourLogsCollectionView.Refresh();
        }

        private void SetTour(Tour? tour) {
            if (tour != null)
                SelectedTour = tour;
        }
    }
}
