using BusinessLayer;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Models;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test;

public class EditTourWindowVMTests {

    private ITourStore _tourStore = A.Fake<TourStore>();
    private IBusinessLogicTours _businessLogicTours = A.Fake<BusinessLogicImp>();
    private IOpenRouteService _openRouteService = A.Fake<BusinessLogicOpenRouteService>();
    [Test]
    public void FinishEditFunction_InvalidTour_Error() {
        // Arrange
        EditTourWindowVM viewModel = new EditTourWindowVM(_tourStore, new WindowStore(), _businessLogicTours, new MessageBoxService(), _openRouteService);

        // Act
        viewModel.FinishEditFunction();

        // Assert
        Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
    }
}