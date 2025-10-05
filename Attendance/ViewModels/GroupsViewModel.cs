using System.Collections.ObjectModel;
using Attendance.Models;
using Attendance.Services.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
namespace Attendance.ViewModels;

public partial class GroupsViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IDatabaseService _databaseService;
    
    [ObservableProperty]
    private ObservableCollection<GroupViewModel> _groups;

    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private string? _firstName;
    
    [ObservableProperty]
    private string? _lastName;

    public GroupsViewModel(INavigationService navigationService, IDatabaseService databaseService)
    {
        _navigationService = navigationService;
        _databaseService = databaseService;
        LoadDataAsync();
    }

    private void LoadDataAsync()
    {
        Groups = new();
        
        foreach (var group in _databaseService.GetGroups())
        {
            var gvm = new GroupViewModel(group, _navigationService, _databaseService);
            Groups.Add(gvm);
        }
        
    }

    [RelayCommand]
    private void AddGroup()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
        {
            return;
        }
        
        _databaseService.AddGroup(new GroupModel(null,Name, Description));

        LoadDataAsync();
        Name = "";
        Description = "";
    }

    [RelayCommand]
    private void AddPerson()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            return;
        }
        
        var person = new PersonModel(FirstName.Trim(), LastName.Trim());
        _databaseService.AddPerson(person);

        FirstName = "";
        LastName = "";
    }

    [RelayCommand]
    private void ViewPeople()
    {
        _navigationService.NavigateTo<PeopleViewModel>();
    }
}