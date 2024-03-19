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
                TransportType = "Plane",
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "We can",
                Description = "We can do it",
                StartLocation = "New York",
                EndLocation = "LA",
                TransportType = "Plane",
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Yooo",
                Description = "Yooo",
                StartLocation = "Berlin",
                EndLocation = "Munich",
                TransportType = "Car",
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Austria",
                Description = "Austria is a country",
                StartLocation = "Vienna",
                EndLocation = "Salzburg",
                TransportType = "Car",
                RouteInformationImage = @"..\Assets\Images\Tour.png"
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
                    EditTourEvent?.Invoke(this, _selectedTour);
                    SelectedTourEvent?.Invoke(this, _selectedTour);
                }
            }
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

        public event EventHandler<Tour>? EditTourEvent;
        public event EventHandler<Tour>? SelectedTourEvent;

        public RelayCommand AddTourCommand { get; }
        public RelayCommand DeleteTourCommand { get; }
        public RelayCommand EditTourCommand { get; }

        public TourListVM() {
            AddTourCommand = new RelayCommand((_) => OpenAddTour());
            DeleteTourCommand = new RelayCommand(DeleteTour);
            EditTourCommand = new RelayCommand(OpenEditTour);
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

        private void OpenEditTour(object? a) {
            if (a is Tour tour) {
                EditTourWindow editTourWindow = new();
                EditTourWindowVM editTourWindowVM = new(ref tour, editTourWindow);
                editTourWindow.DataContext = editTourWindowVM;
                //editTourWindowVM.EditTourEvent += (s, e) => EditTour(e);
                editTourWindow.Show();
            }
        }

        private void EditTour(Tour tour) {
            int index = TourList.IndexOf(tour);
            TourList[index] = tour;
            SelectedTour = tour;
        }

        private void DeleteTour(object? a) {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this tour?", "Delete Tour", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (a is Tour tour && result == MessageBoxResult.Yes) {
                TourList.Remove(tour);
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
            TourList.Add(tour);
            SelectedTour = tour;
        }
    }
}
