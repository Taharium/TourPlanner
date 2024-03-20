using Tour_Planner.Models;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class AddTourWindowVMTests {
        [Test]
        public void AddFunction_InvalidTour_ShowsErrorMessage() {
            // Arrange
            var viewModel = new AddTourWindowVM(null);
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
