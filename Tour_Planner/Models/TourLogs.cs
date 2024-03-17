using System;
using Tour_Planner.ViewModels;
using Tour_Planner.Enums;

namespace Tour_Planner.Models {
    public class TourLogs : ViewModelBase {
        private DateTime dateTime = DateTime.Now;
        private string totalTime = "";
        private string distance = "";
        private Rating rating;
        private string comment = "";
        private Difficulty difficulty;
        public DateTime DateTime {
            get => dateTime;
            set {
                if (dateTime != value) {
                    dateTime = value;
                    RaisePropertyChanged(nameof(DateTime));
                }
            }
        }

        public string TotalTime {
            get => totalTime;
            set {
                if (totalTime != value) {
                    totalTime = value;
                    RaisePropertyChanged(nameof(TotalTime));
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

        public Rating Rating {
            get => rating;
            set {
                if (rating != value) {
                    rating = value;
                    RaisePropertyChanged(nameof(Rating));
                }
            }
        }

        public string Comment {
            get => comment;
            set {
                if (comment != value) {
                    comment = value;
                    RaisePropertyChanged(nameof(Comment));
                }
            }
        }

        public Difficulty Difficulty {
            get => difficulty;
            set {
                if (difficulty != value) {
                    difficulty = value;
                    RaisePropertyChanged(nameof(Difficulty));
                }
            }
        }
    }
}
