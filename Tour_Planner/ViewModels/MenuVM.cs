using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    private readonly IWindowService<GeneratePdfWindowVM, GeneratePdfWindow> _generatepdfWindow;
    private readonly ITourStore _tourStore;
    
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
    public RelayCommand GenerateReportCommand { get; }

    public MenuVM(IBusinessLogicTours businessLogicTours, IWindowService<ImportTourWindowVM, ImportTourWindow> importTourWindow, ITourStore tourStore,
        IWindowService<ExportTourWindowVM, ExportTourWindow> exportTourWindow,
        IWindowService<GeneratePdfWindowVM, GeneratePdfWindow> generatepdfWindow) {
        _businessLogicTours = businessLogicTours;
        _importTourWindow = importTourWindow;
        _exportTourWindow = exportTourWindow;
        _generatepdfWindow = generatepdfWindow;
        tourStore.OnSelectedTourChangedEvent += SetTour;
        _tourStore = tourStore;
        _tour = _tourStore.CurrentTour;
        
        GenerateReportCommand = new RelayCommand((_) => OpenGeneratePdfWindow(), (_) => CanExecuteExportGenerateTour());
        ExportTourCommand = new RelayCommand((_) => OpenExportOneWindow(), (_) => CanExecuteExportGenerateTour());
        ImportTourCommand = new RelayCommand((_) => OpenImportWindow());
    }

    private void OpenGeneratePdfWindow() {
        _generatepdfWindow.ShowDialog();
    }

    private void OpenImportWindow() {
        _importTourWindow.ShowDialog();
    }

    private void OpenExportOneWindow() {
        _exportTourWindow.ShowDialog();
    }

    private bool CanExecuteExportGenerateTour()
    {
        return _tourStore.Tours.Any();
    }

    private void SetTour(Tour? tour) {
        if (tour != null)
            SelectedTour = tour;
    }

}