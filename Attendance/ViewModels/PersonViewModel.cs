using System;
using System.Collections.Generic;
using System.Linq;
using Attendance.Enums;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class PersonViewModel : ViewModelBase
{
    private readonly IDatabaseService _databaseService;
    private readonly INavigationService _navigationService;

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
    
    public PersonViewModel(int id, string firstName, string lastName, GroupViewModel group, List<PersonState> states, IDatabaseService databaseService, INavigationService navigationService)
    {
        _databaseService = databaseService;
        _navigationService  = navigationService;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Group = group;
        States = states;
    }

    [RelayCommand]
    private void Remove()
    {
        if (States.Contains(PersonState.Remove)) return;
        _databaseService.DeletePersonWith(Id);
        _navigationService.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void RemoveFromGroup()
    {
        if (States.Contains(PersonState.RemovableFromGroup)) return;

        _databaseService.DeletePersonFromGroup(Id, Group.Id);
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
        _navigationService.NavigateTo<EditPersonViewModel, EditPersonParameters>(new() {Person = this});
    }
}