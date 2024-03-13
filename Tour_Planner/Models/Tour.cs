namespace Tour_Planner.Models {
    public class Tour {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string StartLocation { get; set; } = "";
        public string EndLocation { get; set; } = "";
        public string TransportType { get; set; } = "";
        public string RouteInformationImage { get; set; } = ""; //from API
        public string Distance { get; set; } = ""; //from API
        public string EstimatedTime { get; set; } = ""; //from API
    }
}
