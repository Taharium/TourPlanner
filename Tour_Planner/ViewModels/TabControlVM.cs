using Tour_Planner.Enums;
using Tour_Planner.Models;

namespace Tour_Planner.ViewModels {

    public class TabControlVM : ViewModelBase {
        private int _selectedTab;
        private Tour _tour;

        public Tour Tour {
            get => _tour;
            set {
                if (_tour != value) {
                    _tour = value;
                    RaisePropertyChanged(nameof(Tour));
                }
            }
        }



        public int SelectedTab {
            get => _selectedTab;
            set {
                if (_selectedTab != value) {
                    _selectedTab = value;
                    RaisePropertyChanged(nameof(SelectedTab));
                }
            }
        }

        public TabControlVM() {
            _tour = new Tour();
            _selectedTab = (int)TabControlEnum.General;
        }

        public void SetTour(Tour tour) {
            SelectedTab = (int)TabControlEnum.General;
            Tour = tour;
        }



    }
}
