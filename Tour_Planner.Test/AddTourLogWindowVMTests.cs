using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using FakeItEasy;
using Models;
using Models.Enums;
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
        public async Task AddTourLog_InValidData_Error() {
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
            await viewModel.AddTourLogFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please enter a valid number for Distance!"));
        }
        
        [Test]
        public async Task AddTourLogFunction_BusinessLayerException_MessageBoxAndWindowNotClosed()
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
            await viewModel.AddTourLogFunction();

            // Assert
            A.CallTo(() => _messageBoxService.Show("Business Layer Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)).MustHaveHappened();
            A.CallTo(() => _windowStore.Close()).MustHaveHappened();
        }

    }
}
