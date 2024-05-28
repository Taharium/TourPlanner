using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services;

public abstract class TourServiceBase
{
    protected TourDTO ConvertToTourDTO(Tour tour)
    {
        //TODO: Convert Tourlogs habe ich wahrscheinlich falsch gemacht
        return new TourDTO()
        {
            Id = tour.Id,
            Name = tour.Name,
            Description = tour.Description,
            StartLocation = tour.StartLocation,
            EndLocation = tour.EndLocation,
            Distance = tour.Distance,
            EstimatedTime = tour.EstimatedTime,
            TransportType = tour.TransportType,
            Popularity = tour.Popularity,
            ChildFriendliness = tour.ChildFriendliness,
            Directions = tour.Directions,
            TourLogsList = ConvertToTourLogsDTO(tour.Id, tour.TourLogsList)
        };
    }
    
    protected Tour ConvertToTourModel(TourDTO tourDTO)
    {
        return new Tour()
        {
            Id = tourDTO.Id,
            Name = tourDTO.Name,
            Description = tourDTO.Description,
            StartLocation = tourDTO.StartLocation,
            EndLocation = tourDTO.EndLocation,
            Distance = tourDTO.Distance,
            EstimatedTime = tourDTO.EstimatedTime,
            TransportType = tourDTO.TransportType,
            Popularity = tourDTO.Popularity,
            ChildFriendliness = tourDTO.ChildFriendliness,
            Directions = tourDTO.Directions,
            TourLogsList = new(ConvertToTourLogsModel(tourDTO.TourLogsList))
        };
    }

    private List<TourLogsDTO> ConvertToTourLogsDTO(int tourId, ICollection<TourLogs> tourLogs) {
        List<TourLogsDTO> tourLogsDtos = [];
        
        foreach (var tourLog in tourLogs) {
            tourLogsDtos.Add(new TourLogsDTO() {
                Id = tourLog.Id,
                //TourId = tourId,
                DateTime = tourLog.DateTime,
                TotalTime = tourLog.TotalTime,
                Distance = tourLog.Distance,
                Comment = tourLog.Comment,
                Difficulty = tourLog.Difficulty,
                Rating = tourLog.Rating,
            });
        }

        return tourLogsDtos;
    }

    private List<TourLogs> ConvertToTourLogsModel(ICollection<TourLogsDTO> tourLogsDTO) {
        List<TourLogs> tourLogsList = [];
        
        foreach (var tourLogDTO in tourLogsDTO) {
            tourLogsList.Add(new TourLogs() {
                Id = tourLogDTO.Id,
                DateTime = tourLogDTO.DateTime,
                TotalTime = tourLogDTO.TotalTime,
                Distance = tourLogDTO.Distance,
                Comment = tourLogDTO.Comment,
                Difficulty = tourLogDTO.Difficulty,
                Rating = tourLogDTO.Rating,
            });
        }

        return tourLogsList;
    }
    
    public TourLogsDTO ConvertToTourLogsDTO(Tour tourDto, TourLogs tourLog) {
        TourLogsDTO tourLogsDto = new TourLogsDTO() {
            Id = tourLog.Id,
            //TourId = tourId,
            DateTime = tourLog.DateTime,
            TotalTime = tourLog.TotalTime,
            Distance = tourLog.Distance,
            Comment = tourLog.Comment,
            Difficulty = tourLog.Difficulty,
            Rating = tourLog.Rating,
        };
        return tourLogsDto;
    }
    
    public TourLogs ConvertToTourLogsModel(TourLogsDTO tourLogDTO) {
        TourLogs tourLogs = new TourLogs() {
            Id = tourLogDTO.Id,
            DateTime = tourLogDTO.DateTime,
            TotalTime = tourLogDTO.TotalTime,
            Distance = tourLogDTO.Distance,
            Comment = tourLogDTO.Comment,
            Difficulty = tourLogDTO.Difficulty,
            Rating = tourLogDTO.Rating,
        };
        return tourLogs;
    }
}