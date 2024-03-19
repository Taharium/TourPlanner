﻿using System;
using Tour_Planner.Enums;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Models {
    public class TourLogs : ViewModelBase {
        private DateTime? dateTime;
        private string totalTime = "";
        private string distance = "";
        private Rating rating = Rating.Excellent;
        private string comment = "";
        private Difficulty difficulty = Difficulty.Easy;

        public DateTime? DateTime {
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

        public TourLogs() { }

        public TourLogs(TourLogs tourLog) {
            DateTime = tourLog.DateTime;
            TotalTime = tourLog.TotalTime;
            Distance = tourLog.Distance;
            Rating = tourLog.Rating;
            Comment = tourLog.Comment;
            Difficulty = tourLog.Difficulty;
        }


    }
}
