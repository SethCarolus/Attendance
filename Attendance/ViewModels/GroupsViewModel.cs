using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Attendance.Factories;
using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class GroupsViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;
    private readonly IDialogService _dialog;

    private readonly IGroupViewModelFactory _groupViewModelFactory;

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

    public GroupsViewModel(INavigationService navigation, IDatabaseService database, IGroupViewModelFactory groupViewModelFactory, IDialogService dialog)
    {
        _navigation = navigation;
        _database = database;
        _groupViewModelFactory = groupViewModelFactory;
        _dialog = dialog;
        LoadDataAsync();
    }

    public GroupsViewModel()
    {
    }

    private void LoadDataAsync()
    {
        Groups = new();
        
        foreach (var group in _database.GetGroups())
        {
            Groups.Add(_groupViewModelFactory.Create(group));
        }
        
    }

    [RelayCommand]
    private void AddGroup()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
        {
            return;
        }
        
        _database.AddGroup(new GroupModel(Name, Description));

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
        _database.AddPerson(person);

        FirstName = "";
        LastName = "";
    }

    [RelayCommand]
    private async Task ViewPeopleAsync()
    {
       _navigation.NavigateTo<PeopleViewModel>();
    }
}