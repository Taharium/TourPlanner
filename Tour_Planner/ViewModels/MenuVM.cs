using System.Collections.Generic;
using System.Linq;
using BusinessLayer;
using Models;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.ViewModels;

public class MenuVM : ViewModelBase {
    private Tour? _tour;
    private readonly IBusinessLogicTours _businessLogicTours;
    private readonly IWindowService<ImportTourWindowVM, ImportTourWindow> _importTourWindow;
    private readonly IWindowService<ExportTourWindowVM, ExportTourWindow> _exportTourWindow;
    public Tour? SelectedTour {
        get => _tour;
        set {
            if (_tour != value) {
                _tour = value;
                OnPropertyChanged(nameof(SelectedTour));
                ExportTourCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public RelayCommand ExportTourCommand { get; }
    public RelayCommand ImportTourCommand { get; }

    public MenuVM(IBusinessLogicTours businessLogicTours, IWindowService<ImportTourWindowVM, ImportTourWindow> importTourWindow, ITourStore tourStore,
        IWindowService<ExportTourWindowVM, ExportTourWindow> exportTourWindow) {
        _businessLogicTours = businessLogicTours;
        _importTourWindow = importTourWindow;
        _exportTourWindow = exportTourWindow;
        tourStore.OnSelectedTourChangedEvent += SetTour;
        _tour = tourStore.CurrentTour;
        
        ExportTourCommand = new RelayCommand((_) => OpenExportOneWindow(), (_) => CanExecuteExportTour());
        ImportTourCommand = new RelayCommand((_) => OpenImportWindow());
    }

    private void OpenGeneratePdfWindow() {
    }

    private void OpenImportWindow() {
        _importTourWindow.ShowDialog();
    }

    private void OpenExportOneWindow() {
        _exportTourWindow.ShowDialog();
    }

    private bool CanExecuteExportTour() {
        return _businessLogicTours.GetTours().Count() != 0;
    }

    public void SetTour(Tour? tour) {
        if (tour != null)
            SelectedTour = tour;
    }

}