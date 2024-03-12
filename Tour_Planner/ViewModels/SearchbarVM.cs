using System;

namespace Tour_Planner.ViewModels {
    public class SearchbarVM : ViewModelBase {
        private string _searchText = string.Empty;

        public string SearchText {
            get => _searchText;
            set {
                if (_searchText != value) {
                    _searchText = value;
                    RaisePropertyChanged(nameof(SearchText));
                    SearchTextChanged?.Invoke(this, SearchText);
                }
            }
        }

        public RelayCommand SearchCommand { get; }

        public event EventHandler<string>? SearchTextChanged;

        public SearchbarVM() {
            SearchCommand = new RelayCommand((_) => TestFunction());
        }


        void TestFunction() {
            SearchTextChanged?.Invoke(this, SearchText);
        }
    }
}
