using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Attendance.Enums;
using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class GroupViewModel : ViewModelBase
{
    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;
    private readonly IDialogService _dialog;

    private readonly IPersonViewModelFactory _personViewModelFactory;
    
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string? _name;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]   
    private ObservableCollection<PersonViewModel> _allPeople;
    
    
    public GroupViewModel(GroupModel group, IAppContext appContext, IPersonViewModelFactory personViewModelFactory)
    {
        ArgumentNullException.ThrowIfNull(group);

        _navigation = appContext.NavigationService;
        _database = appContext.DatabaseService;
        _dialog = appContext.DialogService;
        _personViewModelFactory = personViewModelFactory;

        Id = group.Id;
        Name = group.Name;
        Description = group.Description;
        
        People = new();
        foreach (var p in group.People)
        {
            People.Add(_personViewModelFactory.Create(p, this, [PersonState.RemovableFromGroup]));
        }
        
        var context =  new AttendanceContext();
        AllPeople = new();
        foreach (var p in context.People.Where(p => !group.People.Contains(p)))
        {
            AllPeople.Add(personViewModelFactory.Create(p, this,[PersonState.Add]));       
        }
    }

    public GroupViewModel()
    {
    }
    
    [RelayCommand]
    private void View()
    {
        _navigation.NavigateTo(this);
    }

    [RelayCommand]
    private void Sessions()
    {
        _navigation.NavigateTo<SessionsViewModel, SessionsParameters>(new() {Group = this});
    }

    [RelayCommand]
    private async Task RemoveAsync()
    {
        var displayName = string.IsNullOrWhiteSpace(Name) ? "Group" : Name;
        _dialog.Show<ConfirmationDialogViewModel>("Attendance",$"Are you sure you want to remove {Name}?");

        var result = await _dialog.CurrentDialogViewModel.Task;
        
        if (!result) return;
        
        _database.DeleteGroupWith(Id);
        _navigation.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        _navigation.NavigateTo<EditGroupViewModel, EditGroupParameters>(new() { Group = this});
    }

    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<GroupsViewModel>();
    }
    
    public void Refresh()
    {
        var group = _database.GetGroupWith(Id);

        People = new();
        foreach (var p in group.People)
        {
            People.Add(_personViewModelFactory.Create(p, this, [PersonState.RemovableFromGroup]));
        }  
        
        AllPeople = new();
        foreach (var p in _database.GetPeople().Where(p => !group.People.Contains(p)))
        {
            AllPeople.Add(_personViewModelFactory.Create(p, this, [PersonState.Add]));       
        }
    }
}