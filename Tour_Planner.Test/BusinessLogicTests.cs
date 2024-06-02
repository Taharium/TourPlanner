using System.Collections.ObjectModel;
using BusinessLayer;
using FakeItEasy;
using Models;
using Models.Enums;

namespace Tour_Planner.Test;

public class BusinessLogicTests
{
    private Tour tour = new Tour();
    private BusinessLogicImp _businessLogicTours = A.Fake<BusinessLogicImp>();
    
    [SetUp]
    public void Setup()
    {
        
        TourLogs log1 = new TourLogs
        {
            DateTime = DateTime.Now,
            TotalTime = "2",
            Distance = "10",
            Rating = Rating.Excellent,
            Comment = "Great tour!",
            Difficulty = Difficulty.Easy
        };

        TourLogs log2 = new TourLogs
        {
            DateTime = DateTime.Now.AddDays(-1),
            TotalTime = "3",
            Distance = "15",
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
            TransportType = TransportType.Foot,
            RouteInformationImage = "path_to_image",
            Distance = "25",
            EstimatedTime = "5",
            TourLogsList = TourLogsList
        };
    }
    
    [Test]
    public void ComputedPopularity()
    {
        // Act
        var popularity = _businessLogicTours.ComputePopularity(tour);
        
        // Assert
        Assert.That(popularity, Is.EqualTo(Popularity.Unpopular));
        
    }
    
    [Test]
    public void ComputedChildFriendliness()
    {
        // Act
        var childFriendliness = _businessLogicTours.ComputeChildFriendliness(tour);
        
        // Assert
        Assert.That(childFriendliness, Is.EqualTo(Child_Friendliness.ChildFriendly));
        
    }
}