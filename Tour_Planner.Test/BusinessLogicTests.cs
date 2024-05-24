using System.Collections.ObjectModel;
using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Tour_Planner.Configurations;

namespace Tour_Planner.Test;

public class BusinessLogicTests
{
    private Tour tour = new Tour();
    private IBusinessLogicTours businessLogictours;
    
    [SetUp]
    public void Setup()
    {
        IConfiguration configuration = new ConfigurationManager();
        IConfigOpenRouteService configOpenRouteService = new AppConfiguration(configuration);
        IOpenRouteService openRouteService = new BusinessLogicOpenRouteService(configOpenRouteService);
        businessLogictours = new BusinessLogicImp(openRouteService);
        
        TourLogs log1 = new TourLogs
        {
            DateTime = DateTime.Now,
            TotalTime = "2 hours",
            Distance = "10 km",
            Rating = Rating.Excellent,
            Comment = "Great tour!",
            Difficulty = Difficulty.Easy
        };

        TourLogs log2 = new TourLogs
        {
            DateTime = DateTime.Now.AddDays(-1),
            TotalTime = "3 hours",
            Distance = "15 km",
            Rating = Rating.Good,
            Comment = "Nice tour, a bit challenging.",
            Difficulty = Difficulty.Medium
        };

        ObservableCollection<TourLogs> TourLogsList = new ObservableCollection<TourLogs> { log1, log2 };

        tour = new Tour
        {
            Name = "Mountain Tour",
            Description = "A challenging mountain tour.",
            StartLocation = "Mountain Base",
            EndLocation = "Mountain Peak",
            TransportType = TransportType.FootHiking,
            Popularity = Popularity.High,
            ChildFriendliness = Child_Friendliness.Low,
            RouteInformationImage = "path_to_image",
            Distance = "25 km",
            EstimatedTime = "5 hours",
            TourLogsList = TourLogsList
        };
    }
    
    [Test]
    public void ComputedPopularity()
    {
        
        // Act
        var popularity = businessLogictours.ComputePopularity(tour);
        
        // Assert
        Assert.That(popularity, Is.EqualTo(Popularity.Low));
        
    }
}