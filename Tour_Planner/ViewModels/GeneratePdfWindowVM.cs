using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using BusinessLayer;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels;

public class GeneratePdfWindowVM : ViewModelBase{
    private readonly IWindowStore _windowStore;
    private readonly IMessageBoxService _messageBoxService;
    private readonly ISaveFileDialogService _saveFileDialogService;

    private ObservableCollection<Tour> _tourList;
    private Tour? _selectedTour;
    
    private string _fileName = "";
    private string _filePath = "";
    private string _errorMessage = "";
    private bool _selectAll;

    public bool SelectAll {
        get => _selectAll;
        set {
            if (_selectAll != value) {
                _selectAll = value;
                OnPropertyChanged(nameof(SelectAll));
                UpdateSelection();
            }
        }
    }


    public string ErrorMessage {
        get => _errorMessage;
        set {
            if (value != _errorMessage) {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
    }

    public string FileName {
        get => _fileName;
        set {
            if (_fileName != value) {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }
    }

    public string FilePath {
        get => _filePath;
        set {
            if (_filePath != value) {
                _filePath = value;
                OnPropertyChanged(nameof(FilePath));
            }
        }
    }

    public ObservableCollection<Tour> TourList {
        get => _tourList;
        set {
            if (_tourList != value) {
                _tourList = value;
                OnPropertyChanged(nameof(TourList));
            }
        }
    }
    
    public Tour? SelectedTour {
        get => _selectedTour;
        set {
            if (_selectedTour != value) {
                _selectedTour = value;
                if (_selectedTour != null && !SelectAll) {
                    _selectedTour.IsSelected = true;
                    Debug.WriteLine($"Selected: {SelectedTour?.Name}");
                }
                
                OnPropertyChanged(nameof(SelectedTour));
                OnPropertyChanged(nameof(TourList));
            }
        }
    }

    public RelayCommand GeneratePdfReportCommand { get; }
    public RelayCommand UnSelectCommand { get; }
    
    public GeneratePdfWindowVM(IWindowStore windowStore, ITourStore tourStore, IBusinessLogicTours businessLogicTours, 
        IMessageBoxService messageBoxService, ISaveFileDialogService saveFileDialogService) {
        _windowStore = windowStore;
        _messageBoxService = messageBoxService;
        _saveFileDialogService = saveFileDialogService;
        _tourList = new(businessLogicTours.GetTours());
        _selectedTour = tourStore.CurrentTour;

        GeneratePdfReportCommand = new RelayCommand((_) => GeneratePdfReport());
        UnSelectCommand = new RelayCommand((_) => UnSelectTour());
    }

    private void UnSelectTour() {
        _selectAll = false;
        OnPropertyChanged(nameof(SelectAll));
    }

    private void UpdateSelection() {
        if(SelectedTour != null)
            SelectedTour.IsSelected = false;
        OnPropertyChanged(nameof(TourList));
    }
    
    
    private void GeneratePdfReport() {
        
    }
}