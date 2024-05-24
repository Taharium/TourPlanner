using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class EditTourWindowVMTests {

        [Test]
        public void FinishEditFunction_InvalidTour_Error() {
            // Arrange
            IConfiguration configuration = new ConfigurationManager();
            IConfigOpenRouteService configOpenRouteService = new AppConfiguration(configuration);
            IOpenRouteService openRouteService = new BusinessLogicOpenRouteService(configOpenRouteService);
            EditTourWindowVM viewModel = new EditTourWindowVM(new TourStore(), new WindowStore(), new BusinessLogicImp(openRouteService), new MessageBoxService(), new BusinessLogicOpenRouteService(configOpenRouteService));

            // Act
            viewModel.FinishEditFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
        }
    }
}
