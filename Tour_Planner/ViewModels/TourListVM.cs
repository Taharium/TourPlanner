using BusinessLayer;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; } = new();

        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IOpenRouteService _openRouteService;
        private readonly IWindowService<AddTourWindowVM, AddTourWindow> _addTourWindow;
        private readonly IMessageBoxService _messageBoxService;

        public TourListVM(IBusinessLogicTours businessLogicTours, IWindowService<AddTourWindowVM, AddTourWindow> addTourWindow, IMessageBoxService messageBoxService, IOpenRouteService openRouteService) {
            _openRouteService = openRouteService;
            _businessLogicTours = businessLogicTours;
            _addTourWindow = addTourWindow;
            _messageBoxService = messageBoxService;
            _businessLogicTours.AddTourEvent += AddTour;
            _businessLogicTours.OnTourDeleteEvent += DeleteTour; 
            TourList = new(_businessLogicTours.GetTours());
            TourListCollectionView = new(TourList);
            foreach (var tour in TourList) {
                Debug.WriteLine($"TourListVM: {tour.Name} {tour.Id}");
            }
            AddTourCommand = new RelayCommand((_) => OpenAddTour());
            DeleteTourCommand = new RelayCommand(OnDeleteTour);
            EditTourCommand = new RelayCommand(OpenEditTour);
        }

        private string _searchedTour = "";
        private Tour? _selectedTour;

        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                    Debug.WriteLine($"TourListVM: {SelectedTour?.Name} {SelectedTour?.Id}");
                    SelectedTourEvent?.Invoke(this, SelectedTour);
                }
            }
        }

        public ListCollectionView TourListCollectionView { get; private set; }

        public event EventHandler<Tour?>? SelectedTourEvent;

        public RelayCommand AddTourCommand { get; }
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand EditTourCommand { get; }

        

        public void SearchedTour(string searchedTour) {
            _searchedTour = searchedTour;
            TourListCollectionView ??= new ListCollectionView(TourList);
            TourListCollectionView.Filter = FilterTour;
        }

        private bool FilterTour(object item) {
            if (string.IsNullOrEmpty(_searchedTour))
                return true;
            var tour = (Tour)item;
            return tour.Name.Contains(_searchedTour, StringComparison.OrdinalIgnoreCase);
        }

        private void OpenEditTour(object? a) {
            if (a is Tour tour) {
                EditTourWindow editTourWindow = new();
                EditTourWindowVM editTourWindowVM = new(tour, editTourWindow);
                editTourWindowVM.EditTourEvent += (s, e) => EditTour(e);
                editTourWindow.DataContext = editTourWindowVM;
                editTourWindow.Show();
            }
        }

        private void EditTour(Tour tour) {
            _businessLogicTours.UpdateTour(tour);       //check first if update is successful then update in list
            int index = TourList.IndexOf(tour);
            TourList[index] = new(tour);
            SelectedTour = tour;
            TourListCollectionView.Refresh();
        }

        private void OnDeleteTour(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour?", "Delete Tour", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is Tour tour && result == MessageBoxResult.Yes) {
                _businessLogicTours.DeleteTour(tour);        //check first if delete is successful then remove from list
            }
        }

        private void DeleteTour(Tour tour) {
            TourList.Remove(tour);
            TourListCollectionView.Refresh();
        }

        private void OpenAddTour() {
            /*AddTourWindow addTourWindow = new();
            AddTourWindowVM addTourWindowVM = new AddTourWindowVM(addTourWindow);
            addTourWindow.DataContext = addTourWindowVM;
            addTourWindowVM.AddTourEvent += (s, e) => AddTour(e);
            addTourWindow.Show();*/
            _addTourWindow.ShowDialog();
        }

        private void AddTour(Tour tour) {
            //_businessLogicTours.AddTour(tour);      //check first if add is successful then add in list
            _openRouteService.GetRoute(tour);
            TourList.Add(tour);
            SelectedTour = tour;
            TourListCollectionView.Refresh();
        }
    }
}
