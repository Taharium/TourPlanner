using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using BusinessLayer;
using BusinessLayer.BLException;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList => _tourStore.Tours;

        private readonly IBusinessLogicTours _businessLogicTours;
        private readonly IWindowService<AddTourWindowVM, AddTourWindow> _addTourWindow;
        private readonly IWindowService<EditTourWindowVM, EditTourWindow> _editTourWindow;
        private readonly IMessageBoxService _messageBoxService;
        private readonly ITourStore _tourStore;

        public TourListVM(IBusinessLogicTours businessLogicTours, IWindowService<AddTourWindowVM, AddTourWindow> addTourWindow, 
            IMessageBoxService messageBoxService, ITourStore tourStore, 
            IWindowService<EditTourWindowVM, EditTourWindow> editTourWindow) {
            _editTourWindow = editTourWindow;
            _businessLogicTours = businessLogicTours;
            _addTourWindow = addTourWindow;
            _messageBoxService = messageBoxService;
            _tourStore = tourStore;
            _tourStore.OnTourAddedEvent += AddTour;
            TourListCollectionView = new(TourList);
            AddTourCommand = new RelayCommand((_) => OpenAddTour());
            DeleteTourCommand = new AsyncRelayCommand((_) => OnDeleteTour(), (_) => CanExecuteAddEditDelTour());
            EditTourCommand = new RelayCommand((_) => OpenEditTour(), (_) => CanExecuteAddEditDelTour());
        }

        private bool CanExecuteAddEditDelTour() {
            return SelectedTour != null;
        }

        private string _searchedText = "";
        private Tour? _selectedTour;

        public Tour? SelectedTour {
            get => _selectedTour;
            set {
                if (_selectedTour != value) {
                    _selectedTour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                    DeleteTourCommand.OnExecuteChanged();
                    EditTourCommand.OnCanExecuteChanged();
                    _tourStore.SetCurrentTour(SelectedTour);
                }
            }
        }
        
        public ListCollectionView TourListCollectionView { get; private set; }
        
        public RelayCommand AddTourCommand { get; }
        public AsyncRelayCommand DeleteTourCommand { get; }
        public RelayCommand EditTourCommand { get; }



        public void SearchedTour(string searchedText) {
            _searchedText = searchedText;
            TourListCollectionView ??= new ListCollectionView(TourList);
            TourListCollectionView.Filter = FilterTour;
        }

        private bool FilterTour(object item) {
            if (string.IsNullOrEmpty(_searchedText))
                return true;
            var tour = (Tour)item;
            return tour.Name.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.Description.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.StartLocation.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.EndLocation.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.Distance.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.EstimatedTime.Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.TransportType.ToString().Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.Popularity.ToString().Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.ChildFriendliness.ToString().Contains(_searchedText, StringComparison.OrdinalIgnoreCase) ||
                   tour.TourLogsList.Any(t => t.Comment.Contains(_searchedText, StringComparison.OrdinalIgnoreCase)) ||
                   tour.TourLogsList.Any(t => t.Distance.Contains(_searchedText, StringComparison.OrdinalIgnoreCase)) ||
                   tour.TourLogsList.Any(t => t.TotalTime.Contains(_searchedText, StringComparison.OrdinalIgnoreCase)) ||
                   tour.TourLogsList.Any(t => t.Rating.ToString().Contains(_searchedText, StringComparison.OrdinalIgnoreCase)) ||
                   tour.TourLogsList.Any(t => t.Difficulty.ToString().Contains(_searchedText, StringComparison.OrdinalIgnoreCase)) ||
                   tour.TourLogsList.Any(t => t.DateTime.ToString(CultureInfo.InvariantCulture).Contains(_searchedText, StringComparison.OrdinalIgnoreCase));
        }

        private void OpenEditTour() {
            _editTourWindow.ShowDialog();
        }

        private async Task OnDeleteTour() {
            MessageBoxResult result = _messageBoxService.Show("Are you sure you want to delete this tour?", "Delete Tour", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No, MessageBoxOptions.None);
            if (SelectedTour != null && result == MessageBoxResult.Yes) {
                try {
                   await _businessLogicTours.DeleteTour(SelectedTour);
                }
                catch (BusinessLayerException e) {
                    _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (e.ErrorMessage.StartsWith("Database")) {
                        Environment.Exit(1); 
                    }
                }
            }
        }

        private void OpenAddTour() {
            _addTourWindow.ShowDialog();
        }

        public void AddTour(Tour? tour) {
            SelectedTour = tour;
            TourListCollectionView.Refresh();
        }
    }
}
