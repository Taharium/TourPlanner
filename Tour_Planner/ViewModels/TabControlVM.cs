namespace Tour_Planner.ViewModels {

    public enum TabControl {
        General,
        Route,
        Misc
    }

    public class TabControlVM : ViewModelBase {
        private TabControl _selectedTab;
        public TabControl SelectedTab {
            get => _selectedTab;
            set {
                if (_selectedTab != value) {
                    _selectedTab = value;
                    RaisePropertyChanged(nameof(SelectedTab));
                }
            }
        }

        public TabControlVM() {
            _selectedTab = TabControl.General;
        }
    }
}
