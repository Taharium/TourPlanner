using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test;

public class EditTourWindowVMTests {

    private IBusinessLogicTours _businessLogicTours = A.Fake<IBusinessLogicTours>();
    private IMessageBoxService _messageBoxService = A.Fake<IMessageBoxService>();
    private IWindowStore _windowStore = A.Fake<IWindowStore>();
    private IOpenRouteService _openRouteService = A.Fake<IOpenRouteService>();
    private ITourStore _tourStore = A.Fake<ITourStore>();
    [Test]
    public void FinishEditFunction_InvalidTour_Error() {
        // Arrange
        EditTourWindowVM viewModel = new EditTourWindowVM(_tourStore, new WindowStore(), _businessLogicTours, new MessageBoxService(), _openRouteService);

        // Act
        viewModel.FinishEditFunction();

        // Assert
        Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
    }
    
    [Test]
    public void EditTourFunction_BusinessLayerException_MessageBoxAndWindowNotClosed()
    {
        // Arrange
        var viewModel = new EditTourWindowVM(_tourStore,_windowStore, _businessLogicTours, _messageBoxService, _openRouteService);
            
        viewModel.Tour = new Tour {
            Id = 0,
            Name = "null",
            Description = "null",
            StartLocation = "null",
            EndLocation = "null",
            TransportType = TransportType.Car,
            Popularity = Popularity.Unpopular,
            ChildFriendliness = Child_Friendliness.NotSuitable,
            RouteInformationImage = "null",
            Distance = "null",
            EstimatedTime = "null",
            IsSelected = false,
            Directions = "null"
        };
        A.CallTo(() => _businessLogicTours.UpdateTour(A<Tour>._)).Throws(new BusinessLayerException("Business Layer Error"));

        // Act
        viewModel.SelectedPlaceStart = "H";
        viewModel.SelectedPlaceEnd = "H";
        viewModel.FinishEditFunction();

        // Assert
        A.CallTo(() => _messageBoxService.Show("Business Layer Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)).MustHaveHappened();
        A.CallTo(() => _windowStore.Close()).MustHaveHappened();
    }
}