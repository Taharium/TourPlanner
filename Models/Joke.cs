using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models;

public class Joke : INotifyPropertyChanged {
    public string SetUp {
        get => _setUp;
        set {
            if (value != _setUp){
                _setUp = value;
                OnPropertyChanged();
            }    
        }
    }

    public string PunchLine {
        get => _punchLine;
        set {
            if (value != _punchLine){
                _punchLine = value;
                OnPropertyChanged();
            }    
        }
    }

    private string _setUp = "";
    private string _punchLine = "";
    
    
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