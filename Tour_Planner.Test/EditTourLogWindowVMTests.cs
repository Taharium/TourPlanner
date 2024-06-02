using BusinessLayer;
using FakeItEasy;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourLogStores;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class EditTourLogWindowVMTests {
        private IBusinessLogicTourLogs _businessLogicTourLogs = A.Fake<BusinessLogicImp>();
        private ITourStore _tourStore = A.Fake<TourStore>();
        [TestCase("2024-03-20T00:00:00", "2", "", (int)Rating.Good, (int)Difficulty.Medium, false)]
        [TestCase("2024-03-20T00:00:00", "2", "100", (int)Rating.Excellent, (int)Difficulty.Medium, true)]
        public void IsTourLogValid_VariousScenarios_ReturnsExpectedResult(string dateTime, string totalTime, string distance, Rating rating, Difficulty difficulty, bool expected) {
            // Arrange
            TourLogStore tourLog = new TourLogStore();
            tourLog.SetCurrentTourLog(new TourLogs() {
                DateTime = DateTime.Parse(dateTime),
                TotalTime = totalTime,
                Distance = distance,
                Rating = rating,
                Difficulty = difficulty
            });
            
            
            var viewModel = new EditTourLogWindowVM(new WindowStore(), _tourStore, tourLog, _businessLogicTourLogs, new MessageBoxService());

            // Act
            var result = viewModel.IsTourLogValid();

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

    }
}
