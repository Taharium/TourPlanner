using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models;

public class Weather : INotifyPropertyChanged {
    public string Temp {
        get => _temp;
        set {
            if (value != _temp){
                _temp = value;
                OnPropertyChanged();
            }
        }
    }

    public string Description {
        get => _description;
        set {
            if (value != _description){
                _description = value;
                OnPropertyChanged();
            }
        }
    }

    public string Date {
        get => _date;
        set {
            if (value != _date){
                _date = value;
                OnPropertyChanged();
            }
        }
    }
    public string Time {
        get => _time;
        set {
            if (value != _time){
                _time = value;
                OnPropertyChanged();
            }
        }
    }
    
    public string Icon {
        get => _icon;
        set {
            if (value != _icon) {
                _icon = value;
                OnPropertyChanged();
            }
        }
    }
    

    private string _temp = "";
    private string _description = "";
    private string _date = "";
    private string _icon = "";
    private string _time = "";

    

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string propertyName = "") {

        ValidatePropertyName(propertyName);

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void ValidatePropertyName(string propertyName) {
        if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
            throw new ArgumentException("Property not found", propertyName);
        }
    }
}