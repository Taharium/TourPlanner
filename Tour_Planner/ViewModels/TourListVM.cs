using BusinessLayer;
using Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; } = new();

        private IBusinessLogicTours _businessLogicTours;

        public TourListVM(IBusinessLogicTours businessLogicTours) {
            _businessLogicTours = businessLogicTours;
            TourList = new(_businessLogicTours.GetTours());
            TourListCollectionView = new(TourList);
            AddTourCommand = new RelayCommand((_) => OpenAddTour());
            DeleteTourCommand = new RelayCommand(DeleteTour);
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

        private void DeleteTour(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour?", "Delete Tour", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is Tour tour && result == MessageBoxResult.Yes) {
                _businessLogicTours.DeleteTour(tour);        //check first if delete is successful then remove from list
                TourList.Remove(tour);
                TourListCollectionView.Refresh();
            }
        }

        private void OpenAddTour() {
            AddTourWindow addTourWindow = new();
            AddTourWindowVM addTourWindowVM = new AddTourWindowVM(addTourWindow);
            addTourWindow.DataContext = addTourWindowVM;
            addTourWindowVM.AddTourEvent += (s, e) => AddTour(e);
            addTourWindow.Show();
        }

        private void AddTour(Tour tour) {
            _businessLogicTours.AddTour(tour);      //check first if add is successful then add in list
            TourList.Add(tour);
            SelectedTour = tour;
            TourListCollectionView.Refresh();
        }
    }
}
