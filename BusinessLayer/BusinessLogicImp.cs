using BusinessLayer.Services.AddTourServices;
using BusinessLayer.Services.GetToursService;
using Models;
using Models.Enums;

namespace BusinessLayer {
    public class BusinessLogicImp : IBusinessLogicTours, IBusinessLogicTourLogs {

        private readonly IOpenRouteService _openRouteService;
        
        private IAddTourService _addTourService;

        private IGetToursService _getToursService;
        
        public BusinessLogicImp(IOpenRouteService openRouteService, IAddTourService addTourService, IGetToursService getToursService)
        {
            _openRouteService = openRouteService;
            _addTourService = addTourService;
            _getToursService = getToursService;
        }
        
        public List<Tour> TourList { get; set; } = [
            new Tour() {
                Name = "Yess",
                Description = "Yess we can",
                StartLocation = "Washington",
                EndLocation = "San Francisco",
                TransportType = TransportType.CarPrivate,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "We can",
                Description = "We can do it",
                StartLocation = "New York",
                EndLocation = "LA",
                TransportType = TransportType.CarPrivate,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Yooo",
                Description = "Yooo",
                StartLocation = "Berlin",
                EndLocation = "Munich",
                TransportType = TransportType.CarPrivate,
                RouteInformationImage = @"..\Assets\Images\Tour.png"
            },
            new Tour() {
                Name = "Austria",
                Description = "Austria is a country",
                StartLocation = "Vienna",
                EndLocation = "Salzburg",
                TransportType = TransportType.Wheelchair,
                RouteInformationImage = @"..\Assets\Images\Tour.png",
                TourLogsList = [new TourLogs() {
                    Distance = "34",
                    TotalTime = "26"
                }, new TourLogs() {
                    Distance = "36",
                    TotalTime = "268"
                }]
            }
        ];

        public async Task<IEnumerable<Tour>> GetTours()
        {
            return await _getToursService.GetTours();
        }

        public async Task AddTour(Tour tour) {
            var jsonNodedirections = await _openRouteService.GetRoute(tour.StartLocation, tour.EndLocation, tour.TransportType);
            tour.Distance = _openRouteService.GetDistance(jsonNodedirections);
            tour.EstimatedTime = _openRouteService.GetEstimatedTime(jsonNodedirections);
            /*TourList.Add(tour);*/
            await _addTourService.AddTour(tour);
            AddTourEvent?.Invoke(tour);
        }

        public async Task DeleteTour(Tour tour) {
            TourList.Remove(tour);
            OnTourDeleteEvent?.Invoke(tour);
        }

        public async Task UpdateTour(Tour tour) {
            var jsonNodedirections = await _openRouteService.GetRoute(tour.StartLocation, tour.EndLocation, tour.TransportType);
            tour.Distance = _openRouteService.GetDistance(jsonNodedirections);
            tour.EstimatedTime = _openRouteService.GetEstimatedTime(jsonNodedirections);
            var index = TourList.IndexOf(tour);
            if (index != -1) {
                TourList[index] = tour;
                OnTourUpdateEvent?.Invoke(tour);
            }
        }

        public event Action<Tour>? AddTourEvent;
        public event Action<Tour>? OnTourDeleteEvent;
        public event Action<Tour>? OnTourUpdateEvent;
        public event Action<TourLogs>? AddTourLogEvent;
        public event Action<TourLogs>? OnTourLogDeleteEvent;
        public event Action<TourLogs>? OnTourLogUpdateEvent;

        public Popularity ComputePopularity(Tour tour)
        {
            return tour.TourLogsList.Count switch
            {
                <= 4 => Popularity.Low, 
                <= 8 => Popularity.Medium, 
                <= 12 => Popularity.High,
                _ => Popularity.VeryHigh
            };
        }
        
        public Child_Friendliness ComputeChildFriendliness(Tour tour)
        {
            if (tour.TourLogsList.Count == 0)
            {
                return Child_Friendliness.Low; 
            }

            double totalDifficulty = 0;
            double totalTime = 0;
            double totalDistance = 0;

            foreach (var log in tour.TourLogsList)
            {
                totalDifficulty += (int)log.Difficulty;
                totalTime += double.Parse(log.TotalTime);
                totalDistance += double.Parse(log.Distance);
            }

            double averageDifficulty = totalDifficulty / tour.TourLogsList.Count;
            double averageTime = totalTime / tour.TourLogsList.Count;
            double averageDistance = totalDistance / tour.TourLogsList.Count;

            return (averageDifficulty, averageTime, averageDistance) switch
            {
                (<= 1, <= 2, <= 30) => Child_Friendliness.VeryHigh,
                (<= 1.7, <= 3, <= 60) => Child_Friendliness.High,
                (<= 2.5, <= 4, <= 100) => Child_Friendliness.Medium,
                _ => Child_Friendliness.Low
            };
            
        }
        
        
        public async Task AddTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            if (index != -1) {
                TourList[index].TourLogsList.Add(tourLog);
                TourList[index].Popularity = ComputePopularity(TourList[index]);
                TourList[index].ChildFriendliness = ComputeChildFriendliness(TourList[index]);
                OnTourUpdateEvent?.Invoke(TourList[index]);
                AddTourLogEvent?.Invoke(tourLog);
            }
        }

        public async Task DeleteTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            if (index != -1) {
                TourList[index].TourLogsList.Remove(tourLog);
                TourList[index].Popularity = ComputePopularity(TourList[index]);
                TourList[index].ChildFriendliness = ComputeChildFriendliness(TourList[index]);
                OnTourUpdateEvent?.Invoke(TourList[index]);
                OnTourLogDeleteEvent?.Invoke(tourLog);
            }
        }

        public async Task UpdateTourLog(Tour tour, TourLogs tourLog) {
            var index = TourList.IndexOf(tour);
            //TourList[index] = new Tour(tour);
            if (index != -1) {
                var logIndex = TourList[index].TourLogsList.IndexOf(tourLog);
                if (logIndex != -1) {
                    TourList[index].TourLogsList[logIndex] = tourLog;
                    TourList[index].Popularity = ComputePopularity(TourList[index]);
                    TourList[index].ChildFriendliness = ComputeChildFriendliness(TourList[index]);
                    OnTourUpdateEvent?.Invoke(TourList[index]);
                    OnTourLogUpdateEvent?.Invoke(tourLog);
                }
            }
        }
    }
}
