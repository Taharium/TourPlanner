namespace Tour_Planner.ViewModels {
    public class MainWindowVM : ViewModelBase {
        private readonly TourListVM _tourListVM;
        private readonly SearchbarVM _searchbarVM;
        public MainWindowVM(TourListVM tourListVM, SearchbarVM searchbarVM) {
            _tourListVM = tourListVM;
            _searchbarVM = searchbarVM;

            _searchbarVM.SearchTextChanged += (s, e) => _tourListVM.SearchedTour(e);
        }
    }
}
