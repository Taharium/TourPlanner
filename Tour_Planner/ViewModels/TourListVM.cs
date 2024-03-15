using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using Tour_Planner.Models;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; set; } = [
            new Tour() {
                Name = "Yess",
                Description = "Yess we can",
                StartLocation = "Washington",
                EndLocation = "San Francisco",
                TransportType = "Plane"
            },
            new Tour() {
                Name = "We can",
                Description = "We can do it",
                StartLocation = "New York",
                EndLocation = "LA",
                TransportType = "Plane"
            },
            new Tour() {
                Name = "Yooo",
                Description = "Yooo",
                StartLocation = "Berlin",
                EndLocation = "Munich",
                TransportType = "Car"
            },
            new Tour() {
                Name = "Austria",
                Description = "Austria is a country in Central Europe",
                StartLocation = "Vienna",
                EndLocation = "Salzburg",
                TransportType = "Car"
            }
        ];

        private string _searchedTour = "";
        private Tour _selectedTour = new();

        public Tour SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    RaisePropertyChanged(nameof(SelectedTour));
                }
            }
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
            if (a is Tour tour) {
                EditTourWindow editTourWindow = new();
                editTourWindow.DataContext = new EditTourWindowVM(ref tour, editTourWindow);
                editTourWindow.Show();
                SelectedTour = tour;
            }
        }

        private void DeleteTour(object? a) {
            /*ConfirmationWindow confirmationWindow = new("Are you sure you want to delete this tour?", "Delete Tour");
            confirmationWindow.DataContext = confirmationWindow;
            bool result = confirmationWindow.ShowDialog() ?? false;*/
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour?", "Delete Tour", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is Tour tour && result == MessageBoxResult.Yes)
                TourList.Remove(tour);
        }

        private void OpenAddTour() {
            AddTourWindow addTourWindow = new();
            addTourWindow.DataContext = new AddTourWindowVM(addTourWindow);
            addTourWindow.Show();
        }

        public void AddTour(Tour tour) {
            TourList.Add(tour);
            SelectedTour = tour;
        }
    }
}
