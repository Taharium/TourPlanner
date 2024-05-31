using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using BusinessLayer.Services.AddTourServices;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Enums;
using Newtonsoft.Json;
using Tour_Planner.Configurations;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    [TestFixture]
    public class AddTourLogWindowVMTests {
        private IBusinessLogicTourLogs _businessLogicTourLogs = A.Fake<IBusinessLogicTourLogs>();
        private IMessageBoxService _messageBoxService = A.Fake<IMessageBoxService>();
        private ITourStore _tourStore = A.Fake<ITourStore>();
        private IWindowStore _windowStore = A.Fake<IWindowStore>();
        
        [Test]
        public void AddTourLog_InValidData_Error() {
            // Arrange
            var viewModel = new AddTourLogWindowVM(_windowStore, _tourStore, _businessLogicTourLogs, _messageBoxService);
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
        public void AddTourLogFunction_BusinessLayerException_MessageBoxAndWindowNotClosed()
        {
            // Arrange
            var viewModel = new AddTourLogWindowVM(_windowStore, _tourStore, _businessLogicTourLogs, _messageBoxService);
            viewModel.TourLogs = new TourLogs {
                Id = 0,
                DateTime = default,
                TotalTime = "3",
                Distance = "2",
                Rating = Rating.Average,
                Comment = "hello",
                Difficulty = Difficulty.Easy
            };
            
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
            A.CallTo(() => _businessLogicTourLogs.AddTourLog(A<Tour>._,A<TourLogs>._)).Throws(new BusinessLayerException("Business Layer Error"));

            // Act
            viewModel.AddTourLogFunction();

            // Assert
            A.CallTo(() => _messageBoxService.Show("Business Layer Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)).MustHaveHappened();
            A.CallTo(() => _windowStore.Close()).MustHaveHappened();
        }

    }
}
