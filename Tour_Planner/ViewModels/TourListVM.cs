using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Tour_Planner.Models;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; set; } = [
            new Tour() { Name = "Tour 1" },
            new Tour() { Name = "Tour 2" },
            new Tour() { Name = "Austria"},
            new Tour() { Name = "Tour 3" }
            ];

        private string _searchedTour = "";

        public void SearchedTour(string searchedTour) {
            _searchedTour = searchedTour;
            _tourListCollectionView ??= new ListCollectionView(TourList);
            _tourListCollectionView.Filter = FilterTour;
        }

        private bool FilterTour(object item) {
            if (string.IsNullOrEmpty(_searchedTour))
                return true;

            var tour = (Tour)item;
            return tour.Name.Contains(_searchedTour, StringComparison.OrdinalIgnoreCase);
        }

        private ListCollectionView? _tourListCollectionView;

        public ListCollectionView TourListCollectionView {
            get {
                _tourListCollectionView ??= new(TourList);
                _tourListCollectionView.Refresh();
                _tourListCollectionView.MoveCurrentTo(null);
                return _tourListCollectionView;
            }
        }

        public RelayCommand AddTourCommand { get; }
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand EditTourCommand { get; }

        public TourListVM() {
            AddTourCommand = new RelayCommand((_) => OpenAddTour());
            DeleteTourCommand = new RelayCommand(DeleteTour);
            EditTourCommand = new RelayCommand(EditTour);
        }

        private void EditTour(object? a) {

        }

        private void DeleteTour(object? a) {
            if (a is Tour tour)
                TourList.Remove(tour);
        }

        private void OpenAddTour() {
            AddTourWindow addTourWindow = new() {
                DataContext = new AddTourWindowVM()
            };
            addTourWindow.Show();
        }
    }
}
