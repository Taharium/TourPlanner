using System.Collections.ObjectModel;
using System.Diagnostics;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {
    public class TourListVM : ViewModelBase {
        public ObservableCollection<Tour> TourList { get; set; } = [
            new Tour() { Name = "Tour 1" },
            new Tour() { Name = "Tour 2" },
            new Tour() { Name = "Tour 3" }
            ];

        public void SearchedTour(string searchedTour) {
            Debug.WriteLine(searchedTour);
        }

        //ListCOLL
    }
}
