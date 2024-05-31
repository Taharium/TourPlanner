using System.Collections.ObjectModel;
using BusinessLayer;
using FakeItEasy;
using Models;
using Models.Enums;
using Tour_Planner.Services.MessageBoxServices;
using Tour_Planner.Services.WindowServices;
using Tour_Planner.Stores.TourStores;
using Tour_Planner.ViewModels;
using Tour_Planner.WindowsWPF;

namespace Tour_Planner.Test;

public class TourListVMTest {
    private TourListVM _tourListVM;
    private List<Tour> _tourList;

    [SetUp]
    public void Setup() {
        // Initialize the dependencies with FakeItEasy
        var businessLogicTours = A.Fake<IBusinessLogicTours>();
        var addTourWindow = A.Fake<IWindowService<AddTourWindowVM, AddTourWindow>>();
        var editTourWindow = A.Fake<IWindowService<EditTourWindowVM, EditTourWindow>>();
        var messageBoxService = A.Fake<IMessageBoxService>();
        var tourStore = A.Fake<ITourStore>();

        _tourListVM = new TourListVM(businessLogicTours, addTourWindow, messageBoxService, tourStore, editTourWindow);

        _tourList = new List<Tour> {
            new Tour { Name = "Tour 1", Description = "Description 1", StartLocation = "Location 1", EndLocation = "Location 2", Distance = "10 km", EstimatedTime = "1 hour", TransportType = TransportType.Car, Popularity = Popularity.Known, ChildFriendliness = Child_Friendliness.CautionAdvised },
            new Tour { Name = "Tour 2", Description = "Description 2", StartLocation = "Location 3", EndLocation = "Location 4", Distance = "20 km", EstimatedTime = "2 hours", TransportType = TransportType.Cycling, Popularity = Popularity.Popoular, ChildFriendliness = Child_Friendliness.ChildFriendly },
        };

        A.CallTo(() => tourStore.Tours).Returns(new ObservableCollection<Tour>(_tourList));

        _tourListVM.AddTour(null);
    }
    
    [Test]
    public void FilterTour_With_No_Filter() {
        // Arrange
        _tourListVM.SearchedTour("");

        // Act
        var filteredList = _tourList.Where(t => _tourListVM.TourListCollectionView.Filter(t)).ToList();

        // Assert
        Assert.That(filteredList.Count, Is.EqualTo(2));
        Assert.That(filteredList[0].Name, Is.EqualTo("Tour 1"));
        Assert.That(filteredList[1].Name, Is.EqualTo("Tour 2"));
    }

    [Test]
    public void FilterTour_Should_Return_True_When_SearchedText_Matches_Tour_Property() {
        // Arrange
        _tourListVM.SearchedTour("Tour 1");

        // Act
        var filteredList = _tourList.Where(t => _tourListVM.TourListCollectionView.Filter(t)).ToList();

        // Assert
        Assert.That(filteredList.Count, Is.EqualTo(1));
        Assert.That(filteredList[0].Name, Is.EqualTo("Tour 1"));
    }
}