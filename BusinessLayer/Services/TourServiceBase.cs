using DataAccessLayer.DTOs;
using Models;

namespace BusinessLayer.Services;

public abstract class TourServiceBase
{
    protected TourDTO ConvertToTourDTO(Tour tour)
    {
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
            //I dont know how to handle the TourLogsList
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
            //I dont know how to handle the TourLogsList
        };
    }
    
    protected TourLogsDTO ConvertToTourLogsDTO(Tour tour, TourLogs tourLog)
    {
        return new TourLogsDTO()
        {
            Id = tourLog.Id,
            TourId = tour.Id,
            DateTime = tourLog.DateTime,
            TotalTime = tourLog.TotalTime,
            Distance = tourLog.Distance,
            Comment = tourLog.Comment,
            Difficulty = tourLog.Difficulty,
            Rating = tourLog.Rating,
        };
    }
    
    protected TourLogs ConvertToTourLogsModel(TourLogsDTO tourLogDTO)
    {
        return new TourLogs()
        {
            Id = tourLogDTO.Id,
            DateTime = tourLogDTO.DateTime,
            TotalTime = tourLogDTO.TotalTime,
            Distance = tourLogDTO.Distance,
            Comment = tourLogDTO.Comment,
            Difficulty = tourLogDTO.Difficulty,
            Rating = tourLogDTO.Rating,
        };
    }
}