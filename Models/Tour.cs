using System.Collections.ObjectModel;
using Tour_Planner.Enums;

namespace Models {
    public class Tour {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ObservableCollection<TourLogs> TourLogsList { get; set; } = new();
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string StartLocation { get; set; } = "";
        public string EndLocation { get; set; } = "";
        public TransportType TransportType { get; set; } = TransportType.CarPrivate;
        public string RouteInformationImage { get; set; } = "";
        public string Distance { get; set; } = "";
        public string EstimatedTime { get; set; } = "";


        public Tour() { }

        public Tour(Tour tour) {
            Id = tour.Id;
            Name = tour.Name;
            Description = tour.Description;
            StartLocation = tour.StartLocation;
            EndLocation = tour.EndLocation;
            TransportType = tour.TransportType;
            RouteInformationImage = tour.RouteInformationImage;
            Distance = tour.Distance;
            EstimatedTime = tour.EstimatedTime;
            TourLogsList = tour.TourLogsList;
        }
    }

}
