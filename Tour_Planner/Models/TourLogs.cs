using Microsoft.VisualBasic;

namespace Tour_Planner.Models {
    public class TourLogs {
        //date/time, comment, difficulty, total distance, total time, and rating
        public required DateAndTime DateTime { get; set; }
        public string TotalTime { get; set; } = "";
        public string Distance { get; set; } = "";
        public string Rating { get; set; } = "";
        public string Comment { get; set; } = "";
        public string Difficulty { get; set; } = "";

    }
}
