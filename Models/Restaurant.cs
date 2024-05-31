using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Models;

public class Restaurant : INotifyPropertyChanged {
    public string Name {
        get => _name;
        set {
            if (value != _name) {
                _name = value;
                OnPropertyChanged();
            }
        }
    }

    public string Rating {
        get => _rating;
        set {
            if (value != _rating) {
                _rating = value;
                OnPropertyChanged();
            }
        }
    }

    public string PriceLevel {
        get => _priceLevel;
        set {
            if (value != _priceLevel) {
                _priceLevel = value;
                OnPropertyChanged();
            }
        }
    }

    public string UserRating {
        get => _userRating;
        set {
            if (value != _userRating) {
                _userRating = value;
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

    private string _name = "";
    private string _rating = "";
    private string _priceLevel = "";
    private string _userRating = "";
    private string _icon = "";
    
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