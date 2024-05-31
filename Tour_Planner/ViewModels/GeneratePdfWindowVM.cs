using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using BusinessLayer;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFolderDialogServices;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.UIException;

namespace Tour_Planner.ViewModels;

public class GeneratePdfWindowVM : ViewModelBase{
    private readonly IWindowStore _windowStore;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IPdfReportGenerationService _pdfReportGenerationService;
    private readonly IOpenFolderDialogService _openFolderDialogService;

    private ObservableCollection<Tour> _tourList;
    private Tour? _selectedTour;

    private string _imagePath = "Assets/Resource/map.png";
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
                if (_selectedTour != null) {
                    _selectedTour.IsSelected = true;
                }

                if (SelectAll && _selectedTour != null) {
                    _selectedTour.IsSelected = false;
                }
                
                OnPropertyChanged(nameof(SelectedTour));
                OnPropertyChanged(nameof(TourList));
            }
        }
    }

    public RelayCommand GeneratePdfReportCommand { get; }
    public RelayCommand UnSelectCommand { get; }
    public RelayCommand WindowClosingCommand { get; }
    
    public GeneratePdfWindowVM(IWindowStore windowStore, ITourStore tourStore, IBusinessLogicTours businessLogicTours, 
        IMessageBoxService messageBoxService, IOpenFolderDialogService openFolderDialogService, 
        IPdfReportGenerationService pdfReportGenerationService) {
        _windowStore = windowStore;
        _messageBoxService = messageBoxService;
        _openFolderDialogService = openFolderDialogService;
        _pdfReportGenerationService = pdfReportGenerationService;
        
        _tourList = new(tourStore.Tours);
        SelectedTour = tourStore.CurrentTour;

        GeneratePdfReportCommand = new RelayCommand((_) => GeneratePdfReport());
        UnSelectCommand = new RelayCommand((_) => UnSelectTour());
        WindowClosingCommand = new RelayCommand((_) => CloseWindow());
    }

    private void CloseWindow() {
        _selectAll = false;
        if (SelectedTour != null) 
            SelectedTour.IsSelected = false;
    }

    private void UnSelectTour() {
        _selectAll = false;
        if (_selectedTour != null) 
            _selectedTour.IsSelected = false;
        OnPropertyChanged(nameof(SelectAll));
    }

    private void UpdateSelection() {
        if(SelectedTour != null)
            SelectedTour.IsSelected = false;
        OnPropertyChanged(nameof(TourList));
    }

    private bool IsOneTourSelected() {
        return SelectedTour is { IsSelected: true };
    }

    private bool ValidateGenerate() {
        if (!SelectAll && !IsOneTourSelected()) {
            ErrorMessage = "Please select one Tour!";
            return false;
        }
        
        if (FileName == "") {
            ErrorMessage = "Please write a file name!";
            return false;
        }

        return true;
    }
    
    private void GeneratePdfReport() {
        if (!ValidateGenerate()) {
            return;
        }
        
        ErrorMessage = "";
        try {
            bool? dialog = _openFolderDialogService.ShowDialog();
            if (dialog is true) {
                FilePath = $"{_openFolderDialogService.GetFolderPath()}\\{FileName}.pdf";
            
                if (SelectedTour is { IsSelected: true }) {
                    _pdfReportGenerationService.GenerateOneTourReport(SelectedTour, FilePath);
                }
                else if (SelectAll) {
                    _pdfReportGenerationService.GenerateToursSummaryReport(TourList.ToList(),FilePath);
                }
                
                MessageBoxResult result = _messageBoxService.Show("File saved successfully!\n Do you want to see the file?",
                    "Pdf-Report", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                    MessageBoxOptions.None);
                if (result == MessageBoxResult.Yes) {
                    Process.Start("explorer.exe", FilePath);
                }
                CloseWindow();
                _windowStore.Close();
            }
        }
        catch (UiLayerException e) {
            _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            FilePath = "";
            FileName = "";
        }
        
    }
}