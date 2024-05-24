using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Models.Enums;
using Newtonsoft.Json;

namespace Models {
    public class Tour : INotifyPropertyChanged {
        //private ObservableCollection<TourLogs> _tourLogsList = new ObservableCollection<TourLogs>();
        public ObservableCollection<TourLogs> TourLogsList { get; set; } = new ObservableCollection<TourLogs>();
        private Guid _id = Guid.NewGuid();
        private string _name = "";
        private string _description = "";
        private string _startLocation = "";
        private string _endLocation = "";
        private TransportType _transportType = TransportType.CarPrivate;
        private Popularity _popularity = Popularity.Low;
        private Child_Friendliness _childFriendliness = Child_Friendliness.Low;
        private string _routeInformationImage = "";
        private string _distance = "";
        private string _estimatedTime = "";
        private bool _isSelected;

        public Guid Id {
            get => _id;
            set {
                if (_id != value) {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name {
            get => _name;
            set {
                if (value != _name) {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description {
            get => _description;
            set {
                if (value != _description) {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StartLocation {
            get => _startLocation;
            set {
                if (value != _startLocation) {
                    _startLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EndLocation {
            get => _endLocation;
            set {
                if (value != _endLocation) {
                    _endLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public TransportType TransportType {
            get => _transportType;
            set {
                if (value != _transportType) {
                    _transportType = value;
                    OnPropertyChanged();
                }
            }
        }
        public Popularity Popularity {
            get => _popularity;
            set {
                if (value != _popularity) {
                    _popularity = value;
                    OnPropertyChanged();
                }
            }
        }
        public Child_Friendliness ChildFriendliness {
            get => _childFriendliness;
            set {
                if (value != _childFriendliness) {
                    _childFriendliness = value;
                    OnPropertyChanged();
                }
            }
        }

        public string RouteInformationImage {
            get => _routeInformationImage;
            set {
                if (value != _routeInformationImage) {
                    _routeInformationImage = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Distance {
            get => _distance;
            set {
                if (value != _distance) {
                    _distance = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EstimatedTime {
            get => _estimatedTime;
            set {
                if (value != _estimatedTime) {
                    _estimatedTime = value;
                    OnPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public bool IsSelected {
            get => _isSelected;
            set {
                if (value != _isSelected) {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
        public Tour() { }

        public Tour(Tour tour) {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            StartLocation = tour.StartLocation;
            EndLocation = tour.EndLocation;
            TransportType = tour.TransportType;
            Popularity = tour.Popularity;
            ChildFriendliness = tour.ChildFriendliness;
            RouteInformationImage = tour.RouteInformationImage;
            Distance = tour.Distance;
            EstimatedTime = tour.EstimatedTime;
            TourLogsList = tour.TourLogsList;
            IsSelected = tour.IsSelected;
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {

            ValidatePropertyName(propertyName);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidatePropertyName(string propertyName) {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                throw new ArgumentException("Property not found", propertyName);
            }
        }
    }

}
