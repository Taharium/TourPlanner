using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class AddTourWindowVMTests {
        [Test]
        public void AddFunction_InvalidTour_ShowsErrorMessage() {
            // Arrange
            IConfiguration configuration = new ConfigurationManager();
            IConfigOpenRouteService configOpenRouteService = new AppConfiguration(configuration);
            IOpenRouteService openRouteService = new BusinessLogicOpenRouteService(configOpenRouteService);
            IWindowStore windowStore = new WindowStore();
            IMessageBoxService messageBoxService = new MessageBoxService();
            IBusinessLogicTours businessLogicTours = new BusinessLogicImp(openRouteService);
            var viewModel = new AddTourWindowVM(windowStore, messageBoxService, businessLogicTours, openRouteService);
            var expectedTour = new Tour(); // Empty tour, which is invalid
            bool eventRaised = false;

            // Act
            viewModel.Tour = expectedTour;
            viewModel.AddFunction();

            // Assert
            Assert.IsFalse(eventRaised);
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
        }
    }
}
