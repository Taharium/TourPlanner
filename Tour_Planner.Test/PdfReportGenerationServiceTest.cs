using System.Collections.ObjectModel;
using Models;
using Models.Enums;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.UIException;

namespace Tour_Planner.Test;

public class PdfReportGenerationServiceTest {
    private PdfReportGenerationService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new PdfReportGenerationService();
    }

    [Test]
    public async Task GenerateOneTourReport_ValidTour_CreatesPdf()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Sample Tour",
            Description = "A beautiful tour",
            StartLocation = "Start Point",
            EndLocation = "End Point",
            Distance = "10",
            TransportType = TransportType.Cycling,
            TourLogsList = [
                new TourLogs
                {
                    DateTime = DateTime.Now,
                    TotalTime = "02:30:00",
                    Distance = "10",
                    Difficulty = Difficulty.Easy,
                    Comment = "Nice tour",
                    Rating = Rating.Average
                }
            ]
        };
        var path = "SampleTourReport.pdf";

        // Act
        await _service.GenerateOneTourReport(tour, path);

        // Assert
        Assert.IsTrue(File.Exists(path));
        File.Delete(path);  // Cleanup after test
    }

    [Test]
    public void GenerateToursSummaryReport_ValidTourList_CreatesPdf()
    {
        // Arrange
        var tours = new List<Tour>
        {
            new Tour
            {
                Name = "Tour1",
                Description = "First tour",
                StartLocation = "Point A",
                EndLocation = "Point B",
                Distance = "5",
                TransportType = TransportType.Wheelchair,
                TourLogsList = [
                    new TourLogs
                    {
                        DateTime = DateTime.Now,
                        TotalTime = "01:00:00",
                        Distance = "5",
                        Difficulty = Difficulty.Easy,
                        Comment = "Easy ride",
                        Rating = Rating.Excellent
                    }
                ]
            },
            new Tour
            {
                Name = "Tour2",
                Description = "Second tour",
                StartLocation = "Point C",
                EndLocation = "Point D",
                Distance = "8",
                TransportType = TransportType.Foot,
                TourLogsList = [
                    new TourLogs {
                        DateTime = DateTime.Now,
                        TotalTime = "00:50:00",
                        Distance = "8",
                        Difficulty = Difficulty.Easy,
                        Comment = "Intense run",
                        Rating = Rating.Good
                    }
                ]
            }
        };
        var path = "ToursSummaryReport.pdf";

        // Act
        _service.GenerateToursSummaryReport(tours, path);

        // Assert
        Assert.IsTrue(File.Exists(path));
        File.Delete(path);  // Cleanup after test
    }

    [Test]
    public void GenerateOneTourReport_InvalidPath_ThrowsUiLayerException()
    {
        // Arrange
        var tour = new Tour
        {
            Name = "Sample Tour",
            Description = "A beautiful tour",
            StartLocation = "Start Point",
            EndLocation = "End Point",
            Distance = "10",
            TransportType = TransportType.Cycling,
            TourLogsList = new ObservableCollection<TourLogs>()
        };
        var invalidPath = "Z:\\InvalidPath\\SampleTourReport.pdf";  // Assuming Z: is not a valid drive

        // Act & Assert
        var ex = Assert.ThrowsAsync<UiLayerException>(() => _service.GenerateOneTourReport(tour, invalidPath));
        Assert.That(ex.ErrorMessage, Is.EqualTo("Failed to create the PDF-Report for the specified path!"));
    }

    [Test]
    public void GenerateToursSummaryReport_InvalidPath_ThrowsUiLayerException()
    {
        // Arrange
        var tours = new List<Tour>
        {
            new Tour
            {
                Name = "Tour1",
                Description = "First tour",
                StartLocation = "Point A",
                EndLocation = "Point B",
                Distance = "5",
                TransportType = TransportType.Car,
                TourLogsList = new ObservableCollection<TourLogs>()
            }
        };
        var invalidPath = "Z:\\InvalidPath\\ToursSummaryReport.pdf";

        // Act & Assert
        var ex = Assert.Throws<UiLayerException>(() => _service.GenerateToursSummaryReport(tours, invalidPath));
        Assert.That(ex.ErrorMessage, Is.EqualTo("Failed to create the PDF-Report for the specified path!"));
    }
}