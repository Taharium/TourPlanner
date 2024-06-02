using System.Collections.ObjectModel;
using System.Windows;
using FakeItEasy;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.SaveFileDialogServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test;

public class ExportTourWindowVMTest {

    private ITourStore _tourStore;
    private ExportTourWindowVM _exportTourWindowVm;
    private IMessageBoxService _messageBoxService;
    private ISaveFileDialogService _saveFileDialogService;

    [SetUp]
    public void Setup() {
        _tourStore = A.Fake<ITourStore>();
        _saveFileDialogService = A.Fake<ISaveFileDialogService>();
        _messageBoxService = A.Fake<IMessageBoxService>();
        var tours = new List<Tour>
        {
            new Tour {
                TourLogsList = new ObservableCollection<TourLogs>(),
                Id = 0,
                Name = "null",
                Description = "null",
                StartLocation = "null",
                EndLocation = "null",
                TransportType = TransportType.Car,
                Popularity = Popularity.Unpopular,
                ChildFriendliness = Child_Friendliness.NotSuitable,
                RouteInformationImage = "null",
                Distance = "null",
                EstimatedTime = "null",
                IsSelected = true,
                Directions = "null"
            },
            new Tour {
                TourLogsList = new ObservableCollection<TourLogs>(),
                Id = 0,
                Name = "null",
                Description = "null",
                StartLocation = "null",
                EndLocation = "null",
                TransportType = TransportType.Car,
                Popularity = Popularity.Unpopular,
                ChildFriendliness = Child_Friendliness.NotSuitable,
                RouteInformationImage = "null",
                Distance = "null",
                EstimatedTime = "null",
                IsSelected = false,
                Directions = "null"
            }
        };
        _tourStore.Tours = new (tours);
        _exportTourWindowVm = new ExportTourWindowVM(new WindowStore(),_saveFileDialogService,_messageBoxService, _tourStore);
    }
    
    [Test]
    public void OpenFileExplorerTest() {
        A.CallTo(() => _saveFileDialogService.ShowDialog(A<string>.Ignored)).Returns(true);
        A.CallTo(() => _saveFileDialogService.GetFilePath()).Returns("testPath");
        
        // Act
        _exportTourWindowVm.FileName = "testPath";
        _exportTourWindowVm.OpenFileExplorer();

        // Assert
        A.CallTo(() => _saveFileDialogService.ShowDialog(A<string>.Ignored)).MustHaveHappened();
        A.CallTo(() => _saveFileDialogService.GetFilePath()).MustHaveHappened();
        Assert.That(_exportTourWindowVm.FilePath, Is.EqualTo("testPath"));
        A.CallTo(() => _messageBoxService.Show(
            A<string>.That.Contains("File saved successfully"),
            A<string>.Ignored,
            A<MessageBoxButton>.Ignored,
            A<MessageBoxImage>.Ignored,
            A<MessageBoxResult>.Ignored,
            A<MessageBoxOptions>.Ignored)).MustHaveHappened();
        File.Delete(_exportTourWindowVm.FileName);
    }
    
    [Test]
    public void OpenFileExplorer_InvalidExport_ShowsErrorMessage()
    {
        // Arrange
        _exportTourWindowVm.SelectAll = false;
        _exportTourWindowVm.TourList = _tourStore.Tours;
        foreach (var tour in _exportTourWindowVm.TourList)
        {
            tour.IsSelected = false;
        }

        // Act
        _exportTourWindowVm.FileName = "testPath";
        _exportTourWindowVm.OpenFileExplorer();

        // Assert
        Assert.That(_exportTourWindowVm.ErrorMessage, Is.EqualTo("Please select at least one Tour!"));
        File.Delete(_exportTourWindowVm.FileName);
    }
}