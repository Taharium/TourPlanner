using System;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Models {
    public class Tour : ViewModelBase {
        private string name = "";
        private string description = "";
        private string startLocation = "";
        private string endLocation = "";
        private string transportType = "";
        private string routeInformationImage = @"..\Assets\Images\Tour.png";
        private string distance = "";
        private string estimatedTime = "";

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name {
            get => name;
            set {
                if (name != value) {
                    name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        public string Description {
            get => description;
            set {
                if (description != value) {
                    description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public string StartLocation {
            get => startLocation;
            set {
                if (startLocation != value) {
                    startLocation = value;
                    RaisePropertyChanged(nameof(StartLocation));
                }
            }
        }

        public string EndLocation {
            get => endLocation;
            set {
                if (endLocation != value) {
                    endLocation = value;
                    RaisePropertyChanged(nameof(EndLocation));
                }
            }
        }

        public string TransportType {
            get => transportType;
            set {
                if (transportType != value) {
                    transportType = value;
                    RaisePropertyChanged(nameof(TransportType));
                }
            }
        }

        public string RouteInformationImage {
            get => routeInformationImage;
            set {
                if (routeInformationImage != value) {
                    routeInformationImage = value;
                    RaisePropertyChanged(nameof(RouteInformationImage));
                }
            }
        }

        public string Distance {
            get => distance;
            set {
                if (distance != value) {
                    distance = value;
                    RaisePropertyChanged(nameof(Distance));
                }
            }
        }

        public string EstimatedTime {
            get => estimatedTime;
            set {
                if (estimatedTime != value) {
                    estimatedTime = value;
                    RaisePropertyChanged(nameof(EstimatedTime));
                }
            }
        }

        public Tour() { }

        public Tour(Tour otherTour) {
            Id = otherTour.Id;
            Name = otherTour.Name;
            Description = otherTour.Description;
            StartLocation = otherTour.StartLocation;
            EndLocation = otherTour.EndLocation;
            TransportType = otherTour.TransportType;
            RouteInformationImage = otherTour.RouteInformationImage;
            Distance = otherTour.Distance;
            EstimatedTime = otherTour.EstimatedTime;
        }
    }

}
