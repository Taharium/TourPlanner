using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BusinessLayer;
using Tour_Planner.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels {
    public class AddTourWindowVM : ViewModelBase {
        private Tour _tour;
        private string _errorMessage;
        private TransportType _selectedTransportType;
        private readonly IWindowStore _windowStore;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IBusinessLogicTours _businessLogicTours;

        public string ErrorMessage {
            get => _errorMessage;
            set {
                if (_errorMessage != value) {
                    _errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        public Tour Tour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    OnPropertyChanged(nameof(Tour));
                }
            }
        }

        public TransportType SelectedRating {
            get { return _selectedTransportType; }
            set {
                if (_selectedTransportType != value) {
                    _selectedTransportType = value;
                    OnPropertyChanged(nameof(SelectedRating));
                }
            }
        }

        public IEnumerable<TransportType> TransportTypes {
            get {
                return Enum.GetValues(typeof(TransportType)).Cast<TransportType>();
            }
        }

        public event EventHandler<Tour>? AddTourEvent;
        public RelayCommand FinishAddCommand { get; }


        public AddTourWindowVM(IWindowStore windowStore, IMessageBoxService messageBoxService, IBusinessLogicTours businessLogicTour){
            _errorMessage = "";
            _windowStore = windowStore;
            _messageBoxService = messageBoxService;
            _businessLogicTours = businessLogicTour;
            _tour = new Tour();
            FinishAddCommand = new RelayCommand((_) => AddFunction());
        }

        private bool IsTourValid() {
            return !string.IsNullOrWhiteSpace(_tour.Name) && !string.IsNullOrWhiteSpace(_tour.Description) &&
                   !string.IsNullOrWhiteSpace(_tour.StartLocation) && !string.IsNullOrWhiteSpace(_tour.EndLocation);
        }

        public void AddFunction() {        //could be used using constructor and ref Tour tour
            if (IsTourValid()) {
                ErrorMessage = "";
                _businessLogicTours.AddTour(_tour);
                //AddTourEvent?.Invoke(this, _tour);
                _messageBoxService.Show("Tour added successfully!", "AddTour", MessageBoxButton.OK, MessageBoxImage.Information);
                _windowStore.Close();
            }
            else {
                ErrorMessage = "Please fill in all fields!";
            }
        }

    }
}
