using System.ComponentModel;
using System.Windows.Input;

namespace Tour_Planner {
    internal class TourViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? TourName { get; set; }


        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ExitCommand ExitCommand { get; } = new ExitCommand(); // use command in tag --> Command="{Binding Path=ExitCommand}"
        public ICommand GreetMeCommand { get; }

        public TourViewModel() {
            GreetMeCommand = new RelayCommand((_) => {
                System.Windows.MessageBox.Show("Hello " + TourName);
            });
        }
    }
}
