using BusinessLayer;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    [TestFixture]
    public class AddTourLogWindowVMTests {
        [Test]
        public void AddTourLog_InValidData_Success() {
            // Arrange
            IConfiguration configuration = new ConfigurationManager();
            IConfigOpenRouteService configOpenRouteService = new AppConfiguration(configuration);
            IOpenRouteService openRouteService = new BusinessLogicOpenRouteService(configOpenRouteService);
            var viewModel = new AddTourLogWindowVM(new WindowStore(), new TourStore(), new BusinessLogicImp(openRouteService), new MessageBoxService());
            var tourLog = new TourLogs {
                DateTime = DateTime.Now,
                Comment = "Test comment",
                Difficulty = Difficulty.Easy,
                Distance = "tt",
                TotalTime = "2",
                Rating = Rating.Excellent
            };

            // Act
            viewModel.TourLogs = tourLog;
            viewModel.AddTourLogFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please enter a valid number for Distance"));
        }


    }
}
