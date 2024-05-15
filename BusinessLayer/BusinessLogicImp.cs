using DataAccessLayer;
﻿using System.Diagnostics;
using Models;
using Tour_Planner.Enums;


namespace BusinessLayer {
    public class BusinessLogicImp : IBusinessLogicTours, IBusinessLogicTourLogs {
        public List<Tour> TourList { get; set; } = [
            new Tour() {
                Name = "Yess",
                Description = "Yess we can",
                StartLocation = "Washington",
                EndLocation = "San Francisco",
                TransportType = TransportType.Plane,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "We can",
                Description = "We can do it",
                StartLocation = "New York",
                EndLocation = "LA",
                TransportType = TransportType.Plane,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Yooo",
                Description = "Yooo",
                StartLocation = "Berlin",
                EndLocation = "Munich",
                TransportType = TransportType.Car,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Austria",
                Description = "Austria is a country",
                StartLocation = "Vienna",
                EndLocation = "Salzburg",
                TransportType = TransportType.Car,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            }
        ];

        public IEnumerable<Tour> GetTours()
        {
            return TourList;
        }
        
        

        public void AddTour(Tour tour) {
            TourList.Add(tour);
            AddTourEvent?.Invoke(tour);
        }

        public void DeleteTour(Tour tour) {
            TourList.Remove(tour);
            OnTourDeleteEvent?.Invoke(tour);
        }

        public void UpdateTour(Tour tour) {
            var index = TourList.IndexOf(tour);
            if (index != -1) {
                TourList[index] = tour;
            }
        }

        public event Action<Tour>? AddTourEvent;
        public event Action<Tour>? OnTourDeleteEvent;

        public void AddTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            Debug.WriteLine($"Tour: {tour.Name}, {tour.Id}");
            foreach (var tourl in TourList) {
                Debug.WriteLine($"Tourl: {tourl.Name}, {tourl.Id}");
            }
            if (index != -1) {
                TourList[index].TourLogsList.Add(tourLog);
            }
        }

        public void DeleteTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            if (index != -1) {
                TourList[index].TourLogsList.Remove(tourLog);
            }
        }

        public void UpdateTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            //TourList[index] = new Tour(tour);
            if (index != -1) {
                var logIndex = TourList[index].TourLogsList.IndexOf(tourLog);
                if (logIndex != -1) {
                    TourList[index].TourLogsList[logIndex] = tourLog;
                }
            }
        }
    }
}
