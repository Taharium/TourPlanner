using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using Models;
using Newtonsoft.Json;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFileDialogServices;
using Tour_Planner.Stores.WindowStores;

namespace Tour_Planner.ViewModels;

public class ImportTourWindowVM : ViewModelBase {

    private string _filePath = "";
    private string _errorMessage = ""; 
    private readonly IBusinessLogicTours _businessLogicTours;
    private readonly IMessageBoxService _messageBoxService;
    private IOpenFileDialogService _openFileDialog;
    private readonly IWindowStore _windowStore;


    public string ErrorMessage {
        get => _errorMessage;
        set {
            if (value != _errorMessage) {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
    }

    public string FilePath {
        get => _filePath;
        set {
            if (_filePath != value) {
                _filePath = value;
                ImportCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(FilePath));
            }
        }
    }

    public RelayCommand SearchCommand { get; }
    public RelayCommand ImportCommand { get; }

    public ImportTourWindowVM(IWindowStore windowStore, IMessageBoxService messageBoxService, 
        IBusinessLogicTours businessLogicTour, IOpenFileDialogService openFileDialogService) {
        _windowStore = windowStore;
        _messageBoxService = messageBoxService;
        _businessLogicTours = businessLogicTour;
        _openFileDialog = openFileDialogService;
        SearchCommand = new RelayCommand((_) => OpenFileExplorer());
        ImportCommand = new RelayCommand((_) => ImportFile());
    }

    private void OpenFileExplorer() {
        try {
            bool? dialog = _openFileDialog.ShowDialog();
            if (dialog is true) {
                FilePath = _openFileDialog.GetFilePath();
                if (!File.Exists(FilePath)) {
                    ErrorMessage = "File path does not exist!";
                    return;
                }
            }
            ErrorMessage = "";
        }
        catch (Exception) {
            _messageBoxService.Show("Failed to open File Dialog!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            FilePath = "";
        }
    }

    private bool ValidateImport() {
        if (FilePath == "") {
            ErrorMessage = "Please select a file using the search button!";
            return false;
        }

        return true;
    }

    private void ImportFile() {
        if (!ValidateImport()) {
            return;
        }

        ErrorMessage = "";
        //TODO: Logging
        
        List<Tour> newTours;
        try {
            string jsonfile = File.ReadAllText(FilePath);
            newTours =  JsonConvert.DeserializeObject<List<Tour>>(jsonfile) ?? throw new Exception("There was an error while Se");
        }
        catch (Exception) {
            _messageBoxService.Show("Failed to extract Tours from specified file!", "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
            FilePath = "";
            return;
        }
        ErrorMessage = "";

        try {
            foreach (var tour in newTours) {
                _businessLogicTours.AddTour(tour);
            }
        }
        catch (BusinessLayerException e) {
            _messageBoxService.Show(e.ErrorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            FilePath = "";
            return;
        }
        
        
        _messageBoxService.Show("Import file successfully!", "ImportFile", MessageBoxButton.OK,
            MessageBoxImage.Information);
        _windowStore.Close();
    }
    
}