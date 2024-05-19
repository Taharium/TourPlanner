using Models;
using Tour_Planner.Enums;
using Tour_Planner.Stores.TourStores;

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

        public TabControlVM(ITourStore tourStore) {
            tourStore.OnSelectedTourChangedEvent += SetTour;
            _tour = tourStore.CurrentTour;
            _selectedTab = (int)TabControlEnum.General;
        }

        private void SetTour(Tour? tour) {
            if (tour != null) {
                SelectedTab = (int)TabControlEnum.General;
                SelectedTour = tour;
            }
        }
    }
}
