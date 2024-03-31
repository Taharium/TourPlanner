using Models;
using Tour_Planner.Enums;

namespace Tour_Planner.ViewModels {

    public class TabControlVM : ViewModelBase {
        private int _selectedTab;
        private Tour? _tour;

        public Tour? SelectedTour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }

        public int SelectedTab {
            get => _selectedTab;
            set {
                if (_selectedTab != value) {
                    _selectedTab = value;
                    OnPropertyChanged(nameof(SelectedTab));
                }
            }
        }

        public TabControlVM() {
            _tour = new Tour();
            _selectedTab = (int)TabControlEnum.General;
        }

        public void SetTour(Tour? tour) {
            if (tour != null) {
                SelectedTab = (int)TabControlEnum.General;
                SelectedTour = tour;
            }
        }
    }
}
