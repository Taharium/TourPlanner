using System.Collections.ObjectModel;
using System.Windows;
using BusinessLayer;
using FakeItEasy;
using Models;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFolderDialogServices;
using Tour_Planner.Services.PdfReportGenerationServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test;

public class GeneratePdfWindowVMTest {
    private IWindowStore _windowStore;
    private ITourStore _tourStore;
    private IBusinessLogicTours _businessLogicTours;
    private IMessageBoxService _messageBoxService;
    private IOpenFolderDialogService _openFolderDialogService;
    private IPdfReportGenerationService _pdfReportGenerationService;
    private GeneratePdfWindowVM _viewModel;

    [SetUp]
    public void SetUp()
    {
        _windowStore = A.Fake<IWindowStore>();
        _tourStore = A.Fake<ITourStore>();
        _businessLogicTours = A.Fake<IBusinessLogicTours>();
        _messageBoxService = A.Fake<IMessageBoxService>();
        _openFolderDialogService = A.Fake<IOpenFolderDialogService>();
        _pdfReportGenerationService = A.Fake<IPdfReportGenerationService>();

        var tours = new List<Tour>
        {
            new Tour { Name = "Tour1" },
            new Tour { Name = "Tour2" }
        };
        _tourStore.Tours = new ObservableCollection<Tour>(tours);
        
        A.CallTo(() => _tourStore.CurrentTour).Returns(tours.First());

        _viewModel = new GeneratePdfWindowVM(_windowStore, _tourStore, _businessLogicTours,
                                              _messageBoxService, _openFolderDialogService,
                                              _pdfReportGenerationService);
    }

    [Test]
    public void GeneratePdfReport_ValidSelection_AllToursReportGenerated()
    {
        // Arrange
        _viewModel.SelectAll = true;
        _viewModel.FileName = "AllToursReport";
        A.CallTo(() => _openFolderDialogService.ShowDialog()).Returns(true);

        // Act
        _viewModel.GeneratePdfReport();

        // Assert
        A.CallTo(() => _pdfReportGenerationService.GenerateToursSummaryReport(
            A<List<Tour>>.That.IsSameSequenceAs(_viewModel.TourList.ToList()),
            A<string>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _windowStore.Close()).MustHaveHappenedOnceExactly();
    }
    
}