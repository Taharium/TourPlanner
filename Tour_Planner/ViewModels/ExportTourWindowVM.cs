using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using DataAccessLayer.Logging;
using Models;
using Tour_Planner.Extensions;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using MessageBoxOptions = System.Windows.MessageBoxOptions;

namespace Tour_Planner.ViewModels;

public class ExportTourWindowVM : ViewModelBase {
    private readonly ISaveFileDialogService _saveFileDialog;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IWindowStore _windowStore;
    private ObservableCollection<Tour> _tourList;
    private static readonly ILoggingWrapper Logger = LoggingFactory.GetLogger();

    public ObservableCollection<Tour> TourList {
        get => _tourList;
        set {
            if (_tourList != value) {
                _tourList = value;
                OnPropertyChanged(nameof(TourList));
            }
        }
    }

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
                UpdateSelection(_selectAll);
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

    public RelayCommand SearchAndSaveExportCommand { get; }
    public RelayCommand UnSelectAllCommand { get; }
    public RelayCommand WindowClosingCommand { get; }

    public ExportTourWindowVM(IWindowStore windowStore, ISaveFileDialogService saveFileDialogService, 
        IMessageBoxService messageBoxService, ITourStore tourstore) {
        _windowStore = windowStore;
        _tourList = new(tourstore.Tours);
        _saveFileDialog = saveFileDialogService;
        _messageBoxService = messageBoxService;
        SearchAndSaveExportCommand = new RelayCommand((_) => OpenFileExplorer());
        UnSelectAllCommand = new RelayCommand((_) => UnSelectTours());
        WindowClosingCommand = new RelayCommand((_) => CloseWindow());
    }

    private void CloseWindow() {
        _selectAll = false;
        List<Tour> tours = TourList.Where(t => t.IsSelected).ToList();
        foreach (var tour in tours) {
            tour.IsSelected = false;
        }
    }

    private void UnSelectTours() {
        _selectAll = false;
        List<Tour> tours = TourList.Where(t => t.IsSelected).ToList();
        foreach (var tour in tours) {
            tour.IsSelected = false;
        }
        OnPropertyChanged(nameof(SelectAll));
        OnPropertyChanged(nameof(TourList));
    }

    private void UpdateSelection(bool selectAll) {
        foreach (var tour in TourList) {
            tour.IsSelected = selectAll;
        }
        OnPropertyChanged(nameof(TourList));
    }

    private bool ValidateExport() {
        if (!SelectAll && !IsAnyTourSelected()) {
            ErrorMessage = "Please select at least one Tour!";
            return false;
        }

        if (FileName == "") {
            ErrorMessage = "Please write a file name!";
            return false;
        }

        return true;
    }

    private bool IsAnyTourSelected() {
        return TourList.Any(t => t.IsSelected);
    }

    public void OpenFileExplorer() {
        if (!ValidateExport()) {
            return;
        }

        ErrorMessage = "";

        //TODO: Logging

        List<Tour> tourList;
        tourList = SelectAll ? TourList.ToList() : TourList.Where(t => t.IsSelected).ToList();
        try {
            bool? dialog = _saveFileDialog.ShowDialog(FileName);
            if (dialog is true) {
                FilePath = _saveFileDialog.GetFilePath();
                File.WriteAllText(FilePath, tourList.Beautify());
                MessageBoxResult result = _messageBoxService.Show("File saved successfully!\n Do you want to see the file?",
                    "ExportTour", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                    MessageBoxOptions.None);
                if (result == MessageBoxResult.Yes) {
                    Process.Start("explorer.exe", FilePath);
                }
                CloseWindow();
                _windowStore.Close();
            }
        }
        catch (Exception) {
            Logger.Error($"Failed to Export Tour(s) to specified path: {FilePath}! Please try again using another path!");
            _messageBoxService.Show($"Failed to Export Tour(s) to specified path: {FilePath}! Please try again using another path!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            FilePath = "";
            FileName = "";
        }
        
    }
}