using Tour_Planner.Enums;

namespace Models {
    public class TourLogs {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public string TotalTime { get; set; } = "";
        public string Distance { get; set; } = "";
        public Rating Rating { get; set; } = Rating.Excellent;
        public string Comment { get; set; } = "";
        public Difficulty Difficulty { get; set; } = Difficulty.Easy;

        public TourLogs() { }

        public TourLogs(TourLogs tourLogs) {
            Id = tourLogs.Id;
            DateTime = tourLogs.DateTime;
            TotalTime = tourLogs.TotalTime;
            Distance = tourLogs.Distance;
            Rating = tourLogs.Rating;
            Comment = tourLogs.Comment;
            Difficulty = tourLogs.Difficulty;
        }
    }
}