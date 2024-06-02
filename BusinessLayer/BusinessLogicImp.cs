using BusinessLayer.BLException;
using BusinessLayer.Services.AddTourLogServices;
using BusinessLayer.Services.AddTourServices;
using BusinessLayer.Services.DeleteTourLogServices;
using BusinessLayer.Services.DeleteTourServices;
using BusinessLayer.Services.EditTourLogServices;
using BusinessLayer.Services.EditTourServices;
using BusinessLayer.Services.GetToursServices;
using DataAccessLayer.DALException;
using Models;
using Models.Enums;

namespace BusinessLayer;

public class BusinessLogicImp : IBusinessLogicTours, IBusinessLogicTourLogs {
    private readonly IOpenRouteService _openRouteService;

    private readonly IAddTourService _addTourService;
    private readonly IDeleteTourService _deleteTourService;
    private readonly IEditTourService _editTourService;

    private readonly IAddTourLogService _addTourLogService;
    private readonly IDeleteTourLogService _deleteTourLogService;
    private readonly IEditTourLogService _editTourLogService;

    private IGetToursService _getToursService;

    public event Action<Tour>? AddTourEvent;
    public event Action<Tour>? OnTourDeleteEvent;
    public event Action<Tour>? OnTourUpdateEvent;
    public event Action<TourLogs>? AddTourLogEvent;
    public event Action<TourLogs>? OnTourLogDeleteEvent;
    public event Action<TourLogs>? OnTourLogUpdateEvent;

    public BusinessLogicImp(IOpenRouteService openRouteService, IAddTourService addTourService,
        IGetToursService getToursService, IDeleteTourService deleteTourService, IEditTourService editTourService,
        IAddTourLogService addTourLogService, IDeleteTourLogService deleteTourLogService,
        IEditTourLogService editTourLogService) {
        _openRouteService = openRouteService;
        _getToursService = getToursService;
        _addTourService = addTourService;
        _editTourService = editTourService;
        _deleteTourService = deleteTourService;
        _addTourLogService = addTourLogService;
        _deleteTourLogService = deleteTourLogService;
        _editTourLogService = editTourLogService;
    }

    public Child_Friendliness ComputeChildFriendliness(Tour tour) {
        if (tour.TourLogsList.Count == 0) {
            return Child_Friendliness.NotSuitable;
        }

        double totalDifficulty = 0;
        double totalTime = 0;
        double totalDistance = 0;

        foreach (var log in tour.TourLogsList) {
            totalDifficulty += (int)log.Difficulty;
            if (double.TryParse(log.TotalTime, out double totaltime)) {
                totalTime += totaltime;
            }

            if (double.TryParse(log.Distance, out double totaldistance)) {
                totalDistance += totaldistance;
            }
        }

        double averageDifficulty = totalDifficulty / tour.TourLogsList.Count;
        double averageTime = totalTime / tour.TourLogsList.Count;
        double averageDistance = totalDistance / tour.TourLogsList.Count;

        return (averageDifficulty, averageTime, averageDistance) switch {
            (<= 1, <= 2, <= 30) => Child_Friendliness.HighlyChildFriendly,
            (<= 1.7, <= 3, <= 60) => Child_Friendliness.ChildFriendly,
            (<= 2.5, <= 4, <= 100) => Child_Friendliness.CautionAdvised,
            _ => Child_Friendliness.NotSuitable
        };
    }

    public Popularity ComputePopularity(Tour tour) {
        return tour.TourLogsList.Count switch {
            <= 4 => Popularity.Unpopular,
            <= 8 => Popularity.Known,
            <= 12 => Popularity.Popoular,
            _ => Popularity.VeryPopular
        };
    }

    public async Task<IEnumerable<Tour>> GetTours() {
        try {
            return await _getToursService.GetTours();
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }

    public async Task AddTour(Tour tour) {
        try {
            var jsonNodedirections =
                await _openRouteService.GetRoute(tour.StartLocation, tour.EndLocation, tour.TransportType);
            var directionsstr = jsonNodedirections.ToString();
            tour.Directions = directionsstr;
            tour.Distance = _openRouteService.GetDistance(jsonNodedirections);
            tour.EstimatedTime = _openRouteService.GetEstimatedTime(jsonNodedirections);
            await _addTourService.AddTour(tour);
            AddTourEvent?.Invoke(tour);

        }
        catch (BusinessLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }

    public async Task DeleteTour(Tour tour) {
        try{
            await _deleteTourService.DeleteTour(tour);
            OnTourDeleteEvent?.Invoke(tour);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }

    public async Task UpdateTour(Tour tour) {
        try {
            var jsonNodedirections = await _openRouteService.GetRoute(tour.StartLocation, tour.EndLocation, tour.TransportType);
            var directionsstr = jsonNodedirections.ToString();
            tour.Directions = directionsstr;
            tour.Distance = _openRouteService.GetDistance(jsonNodedirections);
            tour.EstimatedTime = _openRouteService.GetEstimatedTime(jsonNodedirections);
            await _editTourService.EditTour(tour);
            OnTourUpdateEvent?.Invoke(tour);
        }
        catch (BusinessLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }


    public async Task AddTourLog(Tour tour, TourLogs tourLog) {
        try {
            await _addTourLogService.AddTourLog(tour, tourLog);
            await _editTourService.EditTour(tour);
            tour.Popularity = ComputePopularity(tour);
            tour.ChildFriendliness = ComputeChildFriendliness(tour);
            AddTourLogEvent?.Invoke(tourLog);
            OnTourUpdateEvent?.Invoke(tour);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }

    public async Task DeleteTourLog(Tour tour, TourLogs tourLog) {
        try {
            await _deleteTourLogService.DeleteTourLog(tourLog);
            tour.ChildFriendliness = ComputeChildFriendliness(tour);
            tour.Popularity = ComputePopularity(tour);
            OnTourLogDeleteEvent?.Invoke(tourLog);
            OnTourUpdateEvent?.Invoke(tour);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }

    public async Task UpdateTourLog(Tour tour, TourLogs tourLog) {
        try {
            await _editTourLogService.EditTourLog(tourLog);
            tour.ChildFriendliness = ComputeChildFriendliness(tour);
            tour.Popularity = ComputePopularity(tour);
            OnTourLogUpdateEvent?.Invoke(tourLog);
            OnTourUpdateEvent?.Invoke(tour);
        }
        catch (DataLayerException e) {
            throw new BusinessLayerException(e.ErrorMessage);
        }
    }
}