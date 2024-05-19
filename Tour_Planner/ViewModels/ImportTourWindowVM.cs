using System;
using System.IO;
using System.Windows;
using BusinessLayer;
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
    public RelayCommand CloseCommand { get; }

    public ImportTourWindowVM(IWindowStore windowStore, IMessageBoxService messageBoxService, IBusinessLogicTours businessLogicTour, IOpenFileDialogService openFileDialogService) {
        _windowStore = windowStore;
        _messageBoxService = messageBoxService;
        _businessLogicTours = businessLogicTour;
        _openFileDialog = openFileDialogService;
        CloseCommand = new RelayCommand((_) => CloseWindow());
        SearchCommand = new RelayCommand((_) => OpenFileExplorer());
        ImportCommand = new RelayCommand((_) => ImportFile(), (_) => CanExecuteImportFile());
    }

    private bool CanExecuteImportFile() {
        return ErrorMessage == "" && FilePath != "";
    }

    private void OpenFileExplorer() { 
        _openFileDialog = new OpenFileDialogService();
        bool? dialog = _openFileDialog.ShowDialog();
        if (dialog is true) {
            FilePath = _openFileDialog.GetFilePath();
            if (!File.Exists(FilePath)) {
                ErrorMessage = "File path does not exist";
                return;
            }
        }
        ErrorMessage = "";

    }

    private void ImportFile() {
        //TODO: MessageBox or SaveMessage?
        //TODO: Exception handling
        //TODO: Logging
        string jsonfile = File.ReadAllText(FilePath);
        Tour newTour;
        try {
            newTour =  JsonConvert.DeserializeObject<Tour>(jsonfile) ?? throw new Exception();
        }
        catch (Exception) {
            ErrorMessage = "Herp! Herp me!";
            return;
        }

        /*if ( == false) {
            ErrorMessage = "Tour already exists";
            return;
        }*/
        _businessLogicTours.AddTour(newTour);
        ErrorMessage = "";
        _messageBoxService.Show("Import file successfully", "ImportFile", MessageBoxButton.OK,
            MessageBoxImage.Information);
        _windowStore.Close();
    }

    private void CloseWindow() {
        _windowStore.Close();
    }
}