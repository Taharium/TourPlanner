using BusinessLayer;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class AddTourWindowVMTests {
        private IBusinessLogicTours _businessLogicTours = A.Fake<BusinessLogicImp>();
        private IOpenRouteService _openRouteService = A.Fake<BusinessLogicOpenRouteService>();
        
        [Test]
        public void AddFunction_InvalidTour_ShowsErrorMessage() {
            // Arrange
            
            var viewModel = new AddTourWindowVM(new WindowStore(), new MessageBoxService(), _businessLogicTours, _openRouteService);
            var expectedTour = new Tour(); // Empty tour, which is invalid

            // Act
            viewModel.Tour = expectedTour;
            viewModel.AddFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
        }
        
        [Test]
        public async Task AddFunction_InvalidSearchPlaceStart() {
            // Arrange
            
            var viewModel = new AddTourWindowVM(new WindowStore(), new MessageBoxService(), _businessLogicTours, _openRouteService);
            var expectedTour = new Tour() {
                Name = "Blabla",
                Description = "Hello",
                TransportType = TransportType.Car
            };
            
            var start = "Aus";
            // Act
            viewModel.Tour = expectedTour;
            await viewModel.SearchPlaceStart(start);
            //await viewModel.AddFunction();

            // Assert
            Assert.That(viewModel.SelectedPlaceStart, Is.EqualTo(""));
            Assert.That(viewModel.SearchResultsStart.Count, Is.EqualTo(0));
        }
        
        [Test]
        public async Task AddFunction_InvalidSearchPlaceEnd() {
            // Arrange
            
            var viewModel = new AddTourWindowVM(new WindowStore(), new MessageBoxService(), _businessLogicTours, _openRouteService);
            var expectedTour = new Tour() {
                Name = "Blabla",
                Description = "Hello",
                TransportType = TransportType.Car
            };
            
            var end = "Aus";
            // Act
            viewModel.Tour = expectedTour;
            await viewModel.SearchPlaceEnd(end);

            // Assert
            Assert.That(viewModel.SelectedPlaceEnd, Is.EqualTo(""));
            Assert.That(viewModel.SearchResultsEnd.Count, Is.EqualTo(0));
        }
    }
}
