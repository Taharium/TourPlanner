using System.Windows;
using BusinessLayer;
using BusinessLayer.BLException;
using FakeItEasy;
using Models;
using Newtonsoft.Json;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.OpenFileDialogServices;
using Tour_Planner.Stores.WindowStores;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Test;

public class ImportTourWindowVMTest {
    private IWindowStore _windowStore;
    private IMessageBoxService _messageBoxService;
    private IBusinessLogicTours _businessLogicTours;
    private IOpenFileDialogService _openFileDialog;
    private ImportTourWindowVM _importTourWindowVm;

    [SetUp]
    public void SetUp()
    {
        _windowStore = A.Fake<IWindowStore>();
        _messageBoxService = A.Fake<IMessageBoxService>();
        _businessLogicTours = A.Fake<IBusinessLogicTours>();
        _openFileDialog = A.Fake<IOpenFileDialogService>();

        _importTourWindowVm = new ImportTourWindowVM(_windowStore, _messageBoxService, _businessLogicTours, _openFileDialog);
    }

    [Test]
    public void OpenFileExplorer_DialogReturnsTrue_FileDoesNotExist()
    {
        // Arrange
        const string filePath = "testFilePath.json";
        A.CallTo(() => _openFileDialog.ShowDialog()).Returns(true);
        A.CallTo(() => _openFileDialog.GetFilePath()).Returns(filePath);

        // Act
        _importTourWindowVm.OpenFileExplorer();

        // Assert
        Assert.That(_importTourWindowVm.FilePath, Is.EqualTo(filePath));
        Assert.That(_importTourWindowVm.ErrorMessage, Is.EqualTo("File path does not exist!"));
    }
    [Test]
    public void OpenFileExplorer_DialogThrowsException_MessageBoxCalled()
    {
        // Arrange
        A.CallTo(() => _openFileDialog.ShowDialog()).Throws<Exception>();

        // Act
        _importTourWindowVm.OpenFileExplorer();

        // Assert
        Assert.That(_importTourWindowVm.FilePath, Is.EqualTo(""));
        A.CallTo(() => _messageBoxService.Show(
            A<string>.That.Contains("Failed to open File Dialog!"),
            A<string>.Ignored,
            A<MessageBoxButton>.Ignored,
            A<MessageBoxImage>.Ignored)).MustHaveHappened();
    }
    
    [Test]
    public void ImportFile_ValidJson_ImportsAndClosesWindow()
    {
        // Arrange
        var newTours = new List<Tour> { new Tour { Name = "Tour1" }, new Tour { Name = "Tour2" } };
        string jsonContent = JsonConvert.SerializeObject(newTours);
        string filePath = "validjson.json";
        File.WriteAllText(filePath, jsonContent);
        _importTourWindowVm.FilePath = filePath;

        // Act
        _importTourWindowVm.ImportFile();

        // Assert
        A.CallTo(() => _messageBoxService.Show("Import file successfully!", "ImportFile", MessageBoxButton.OK, MessageBoxImage.Information)).MustHaveHappened();
        A.CallTo(() => _windowStore.Close()).MustHaveHappened();
        Assert.That(_importTourWindowVm.ErrorMessage, Is.EqualTo(""));
    }
    
    [Test]
    public void ImportFile_BusinessLayerException_FilePathEmptyAndWindowNotClosed()
    {
        // Arrange
        var newTours = new List<Tour> { new Tour { Name = "Tour1" } };
        string jsonContent = JsonConvert.SerializeObject(newTours);
        string filePath = "validFilePath.json";
        File.WriteAllText(filePath, jsonContent);
        _importTourWindowVm.FilePath = filePath;
        A.CallTo(() => _businessLogicTours.AddTour(A<Tour>._)).Throws(new BusinessLayerException("Business Layer Error"));

        // Act
        _importTourWindowVm.ImportFile();

        // Assert
        A.CallTo(() => _messageBoxService.Show("Business Layer Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error)).MustHaveHappened();
        A.CallTo(() => _windowStore.Close()).MustNotHaveHappened();
        Assert.That(_importTourWindowVm.FilePath, Is.EqualTo(""));
    }
}