namespace Tour_Planner.ViewModels {
    public class MainWindowVM : ViewModelBase {
        private readonly TourListVM _tourListVM;
        private readonly SearchbarVM _searchbarVM;
        private readonly TabControlVM _tabControlVM;
        private readonly TourLogsVM _tourLogsVM;
        public MainWindowVM(TourListVM tourListVM, SearchbarVM searchbarVM, TabControlVM tabControlVM, TourLogsVM tourLogsVM) {
            _tourListVM = tourListVM;
            _searchbarVM = searchbarVM;
            _tabControlVM = tabControlVM;
            _tourLogsVM = tourLogsVM;

            _searchbarVM.SearchTextChanged += (s, e) => _tourListVM.SearchedTour(e);
            _tourListVM.SelectedTourEvent += (s, e) => _tabControlVM.SetTour(e);
            _tourListVM.SelectedTourEvent += (s, e) => _tourLogsVM.SetTour(e);
            _tourLogsVM.Update += (s, e) => _tourListVM.UpdateTourLog(e);
        }
    }
}