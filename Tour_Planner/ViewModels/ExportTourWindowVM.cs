using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    private readonly ISaveFileDialogService _saveFileDialog;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IWindowStore _windowStore;

    private ObservableCollection<Tour> _tourList;

    public ObservableCollection<Tour> TourList {
        get => _tourList;
        set {
            if (_tourList != value) {
                _tourList = value;
                OnPropertyChanged(nameof(TourList));
                SearchAndSaveExportCommand.RaiseCanExecuteChanged();
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
                UpdateSelection(SelectAll);
                SearchAndSaveExportCommand.RaiseCanExecuteChanged();
                Debug.WriteLine($"IsSelected Count: {TourList.Count(t => t.IsSelected)}");
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
                SearchAndSaveExportCommand.RaiseCanExecuteChanged();
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

    public ExportTourWindowVM(IWindowStore windowStore, IBusinessLogicTours businessLogicTours,
        ISaveFileDialogService saveFileDialogService, IMessageBoxService messageBoxService) {
        _windowStore = windowStore;
        _tourList = new(businessLogicTours.GetTours());
        _saveFileDialog = saveFileDialogService;
        _messageBoxService = messageBoxService;
        SearchAndSaveExportCommand = new RelayCommand((_) => OpenFileExplorer());
        UnSelectAllCommand = new RelayCommand((_) => UnSelectTours());
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

    //UNSELECT CHECKBOX
    private void InitializeTourList(IEnumerable<Tour> tours) {
        foreach (var tour in tours) {
            _tourList.Add(new Tour(tour));
        }
    }

    private bool ValidateExport() {
        if (!SelectAll && !IsAnyTourSelected()) {
            ErrorMessage = "Please select at least one Tour";
            return false;
        }

        if (FileName == "") {
            ErrorMessage = "Please write a file name";
            return false;
        }

        return true;
    }

    private bool IsAnyTourSelected() {
        return TourList.Any(t => t.IsSelected);
    }

    private void OpenFileExplorer() {
        if (!ValidateExport()) {
            return;
        }

        ErrorMessage = "";

        List<Tour> tourList;
        if (SelectAll) {
            _messageBoxService.Show("All were selected", "Ex", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            tourList = TourList.ToList();
        }
        else {
            tourList = TourList.Where(t => t.IsSelected).ToList();
        }

        bool? dialog = _saveFileDialog.ShowDialog(FileName);
        if (dialog is true) {
            FilePath = _saveFileDialog.GetFilePath();
            //File.WriteAllText(FilePath, _oneTour ? _tour.Beautify() : _tours.Beautify());
            File.WriteAllText(FilePath, tourList.Beautify());
            //TODO: MessageBox or SaveMessage?
            //TODO: Exception handling
            //TODO: Logging
            MessageBoxResult result = _messageBoxService.Show("File saved successfully!\n Do you want to see the file?",
                "ExportTour", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                MessageBoxOptions.None);
            if (result == MessageBoxResult.Yes) {
                Process.Start("explorer.exe", FilePath);
            }

            _windowStore.Close();
        }
    }
}