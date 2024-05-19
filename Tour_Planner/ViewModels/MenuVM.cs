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
                ExportOneTourCommand.RaiseCanExecuteChanged();
            }
        }
    }

    public RelayCommand ExportOneTourCommand { get; }
    //public RelayCommand ExportAllTourCommand { get; }
    public RelayCommand ImportTourCommand { get; }

    public MenuVM(IBusinessLogicTours businessLogicTours, IWindowService<ImportTourWindowVM, ImportTourWindow> importTourWindow, ITourStore tourStore,
                IWindowService<ExportTourWindowVM, ExportTourWindow> exportTourWindow) {
        _businessLogicTours = businessLogicTours;
        _importTourWindow = importTourWindow;
        _exportTourWindow = exportTourWindow;
        tourStore.OnSelectedTourChangedEvent += SetTour;
        _tour = tourStore.CurrentTour;
        
        ExportOneTourCommand = new RelayCommand((_) => OpenExportOneWindow(), (_) => CanExecuteExportOneTour());
        //ExportAllTourCommand = new RelayCommand((_) => OpenExportWindow());
        ImportTourCommand = new RelayCommand((_) => OpenImportWindow());
    }

    private void OpenImportWindow() {
        _importTourWindow.ShowDialog();
    }

    /*private void OpenExportWindow() {
        ExportTourWindow exportTourWindow = new ExportTourWindow();
        List<Tour> tours = _businessLogicTours.GetTours().ToList();
        ExportTourWindowVM exportTourWindowVm = new ExportTourWindowVM(exportTourWindow, tours);
        exportTourWindow.DataContext = exportTourWindowVm;
        exportTourWindow.ShowDialog();
    }*/

    private void OpenExportOneWindow() {
        _exportTourWindow.ShowDialog();
    }

    private bool CanExecuteExportOneTour() {
        return SelectedTour != null;
    }

    public void SetTour(Tour? tour) {
        if (tour != null)
            SelectedTour = tour;
    }

}