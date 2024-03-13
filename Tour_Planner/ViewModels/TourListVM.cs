using System;
using System.Collections.ObjectModel;
using System.Windows.Data;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; set; } = [
            new Tour() { Name = "Tour 1" },
            new Tour() { Name = "Tour 2" },
            new Tour() { Name = "Austria"},
            new Tour() { Name = "Tour 3" }
            ];

        private string _searchedTour = "";
        public string SearchedTourText {
            get { return _searchedTour; }
            set { _searchedTour = value; }
        }

        public void SearchedTour(string searchedTour) {
            _searchedTour = searchedTour;
            _tourListCollectionView ??= new ListCollectionView(TourList);
            _tourListCollectionView.Filter = FilterTour;
        }

        private bool FilterTour(object item) {
            if (string.IsNullOrEmpty(_searchedTour))
                return true;

            var tour = (Tour)item;
            return tour.Name.IndexOf(_searchedTour, StringComparison.OrdinalIgnoreCase) >= 0;
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
            AddTourCommand = new RelayCommand((_) => AddTour());
            DeleteTourCommand = new RelayCommand((_) => DeleteTour());
            EditTourCommand = new RelayCommand((_) => EditTour());
        }

        private void EditTour() {
            throw new NotImplementedException();
        }

        private void DeleteTour() {
            throw new NotImplementedException();
        }

        private void AddTour() {
            throw new NotImplementedException();
        }
    }
}
