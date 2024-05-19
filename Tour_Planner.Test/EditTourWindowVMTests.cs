using BusinessLayer;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test {
    public class EditTourWindowVMTests {

        [Test]
        public void FinishEditFunction_InvalidTour_Error() {
            // Arrange
            EditTourWindowVM viewModel = new EditTourWindowVM(new TourStore(), new WindowStore(), new BusinessLogicImp(), new MessageBoxService());

            // Act
            viewModel.FinishEditFunction();

            // Assert
            Assert.That(viewModel.ErrorMessage, Is.EqualTo("Please fill in all fields!"));
        }
    }
}
