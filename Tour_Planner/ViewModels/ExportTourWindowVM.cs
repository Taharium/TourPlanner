using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Models;
using System.Windows;
using BusinessLayer;
using Tour_Planner.Extensions;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels;

public class ExportTourWindowVM : ViewModelBase {

    private Tour? _tour;
    //private List<Tour> _tours = null!;
    private readonly IBusinessLogicTours _businessLogicTours;
    private readonly ISaveFileDialogService _saveFileDialog;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IWindowStore _windowStore;

    private string _title = "";
    private string _fileName = "";
    private string _filePath = "";
    private string _errorMessage = "";

    private readonly bool _oneTour;

    public string Title {
        get => _title;
        set {
            if (value != _title) {
                _title = value;
                OnPropertyChanged();
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
    
    

    public Tour? Tour {
        get => _tour;
        set {
            if (_tour != value) {
                _tour = value;
                OnPropertyChanged(nameof(Tour));
            }
        }
    }
    
    public RelayCommand SearchAndSaveExportCommand { get; }
    public RelayCommand CloseCommand { get; }
    public ExportTourWindowVM(IWindowStore windowStore, ITourStore tourStore, IBusinessLogicTours businessLogicTours, ISaveFileDialogService saveFileDialogService, IMessageBoxService messageBoxService) {
        _tour = tourStore.CurrentTour;
        _windowStore = windowStore;
        _businessLogicTours = businessLogicTours;
        _saveFileDialog = saveFileDialogService;
        _messageBoxService = messageBoxService;
        _oneTour = true;
        Title = $"Export one Tour ({Tour?.Name})";
        SearchAndSaveExportCommand = new RelayCommand((_) => OpenFileExplorer());
        CloseCommand = new RelayCommand((_) => CloseWindow());
    }
    
    /*public ExportTourWindowVM(Window window, List<Tour> tours) {
        _window = window;
        _tours = tours;
        Title = "Export all Tours";
        SearchAndSaveExportCommand = new RelayCommand((_) => OpenFileExplorer());
        CloseCommand = new RelayCommand((_) => CloseWindow());
    }*/

    private void OpenFileExplorer() {
        bool? dialog = _saveFileDialog.ShowDialog(FileName);
        if(dialog is true) {
            FilePath = _saveFileDialog.GetFilePath();
            //File.WriteAllText(FilePath, _oneTour ? _tour.Beautify() : _tours.Beautify());
            File.WriteAllText(FilePath, _tour.Beautify());
            ErrorMessage = "";
            //TODO: MessageBox or SaveMessage?
            //TODO: Exception handling
            //TODO: Logging
            MessageBoxResult result = _messageBoxService.Show("File saved successfully!\n Do you want to see the file?", "ExportTour", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No, MessageBoxOptions.None);
            if (result == MessageBoxResult.Yes) {
                Process.Start("explorer.exe", FilePath);
            }
                
            _windowStore.Close();
        }
    }

    private void CloseWindow() {
        _windowStore.Close();
    }
}