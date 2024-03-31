using Models;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class EditTourWindowVMTests {

        [Test]
        public void FinishEditFunction_InvalidTour_Error() {
            // Arrange
            Tour tour = new Tour();
            EditTourWindowVM viewModel = new EditTourWindowVM(tour, null);

            // Act
            viewModel.FinishEditFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
        }
    }
}
