using BusinessLayer;
using BusinessLayer.Services.AddTourServices;
using FakeItEasy;
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
        private IBusinessLogicTourLogs _businessLogicTourLogs = A.Fake<BusinessLogicImp>();
        private ITourStore _tourStore = A.Fake<TourStore>();
        
        [Test]
        public void AddTourLog_InValidData_Success() {
            // Arrange
            var viewModel = new AddTourLogWindowVM(new WindowStore(), _tourStore, _businessLogicTourLogs, new MessageBoxService());
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
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please enter a valid number for Distance!"));
        }

        [Test]
        public void AddTourLog_ValidData_Success() {
            var viewModel = new AddTourLogWindowVM(new WindowStore(), _tourStore, _businessLogicTourLogs, new MessageBoxService());
            var tourLog = new TourLogs {
                DateTime = DateTime.Now,
                Comment = "Test comment",
                Difficulty = Difficulty.Easy,
                Distance = "2",
                TotalTime = "2",
                Rating = Rating.Excellent
            };
            
            // Act
            viewModel.TourLogs = tourLog;
            viewModel.AddTourLogFunction();
            
            //Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo(""));

        }

    }
}
