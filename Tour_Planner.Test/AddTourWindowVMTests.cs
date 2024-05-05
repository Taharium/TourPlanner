using BusinessLayer;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class AddTourWindowVMTests {
        [Test]
        public void AddFunction_InvalidTour_ShowsErrorMessage() {
            // Arrange
            IWindowStore windowStore = new WindowStore();
            IMessageBoxService messageBoxService = new MessageBoxService();
            IBusinessLogicTours businessLogicTours = new BusinessLogicImp();
            var viewModel = new AddTourWindowVM(windowStore, messageBoxService, businessLogicTours);
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
