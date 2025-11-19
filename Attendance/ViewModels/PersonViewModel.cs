using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attendance.Enums;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class PersonViewModel : ViewModelBase
{
    private readonly IDatabaseService _database;
    private readonly INavigationService _navigation;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _firstName;
    
    [ObservableProperty]
    private string _lastName;
    
    [ObservableProperty]
    private GroupViewModel _group; // Group Person is in (InGroup == True) or Group Person could be added to (InGroup == False).
    
    [ObservableProperty]
    private List<PersonState> _states;
    
    private bool IsRemovableFromGroup => States.Contains(PersonState.RemovableFromGroup);
    private bool CanRemove => States.Contains(PersonState.Remove);
    private bool CanAdd => States.Contains(PersonState.Add);
    private bool CanEdit => States.Contains(PersonState.Edit);

    public PersonViewModel()
    {
    }
    
    public PersonViewModel(int id, string firstName, string lastName, GroupViewModel group, List<PersonState> states, IAppContext appContext)
    {
        _database = appContext.DatabaseService;
        _navigation  = appContext.NavigationService;
        _dialog = appContext.DialogService;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Group = group;
        States = states;
    }

    [RelayCommand]
    private async Task RemoveAsync()
    {
        if (!States.Contains(PersonState.Remove)) return;

        _dialog.Show<ConfirmationDialogViewModel>("Attendance", $"Are you sure you want to remove {FirstName} {LastName}?");
        var result = await _dialog.CurrentDialogViewModel.Task;
        if (!result) return;

        _database.DeletePersonWith(Id);
        _navigation.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private async Task RemoveFromGroupAsync()
    {
        if (!States.Contains(PersonState.RemovableFromGroup)) return;

        _dialog.Show<ConfirmationDialogViewModel>("Attendance", $"Are you sure you want to remove {FirstName} {LastName}?");

        var result = await _dialog.CurrentDialogViewModel.Task;

        if (!result) return;

        _database.DeletePersonFromGroup(Id, Group.Id);
        Group.Refresh();
    }
    
    [RelayCommand]
    private void AddToGroup()
    {
        if (!States.Contains(PersonState.Add)) return;
        
        var context = new AttendanceContext();

        var group = context.Groups.FirstOrDefault(g => g.Id == Group.Id);
        if (group == null) return;

        var person = context.People.FirstOrDefault(p => p.Id == Id);
        
        if (person == null) return;

        person.Groups ??= new();

        person.Groups.Add(group);
        context.SaveChanges();
        Group.Refresh();
    }

    [RelayCommand]
    private void Edit()
    {
        _navigation.NavigateTo<EditPersonViewModel, EditPersonParameters>(new() {Person = this});
    }
}