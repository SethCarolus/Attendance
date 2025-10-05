using System;
using System.Linq;
using Attendance.Enums;
using Attendance.Models;
using Attendance.Services.Contracts;
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
    private PersonState _state;
    
    public PersonViewModel(int id, string firstName, string lastName, GroupViewModel group, PersonState state, IDatabaseService databaseService, INavigationService navigationService)
    {
        _databaseService = databaseService;
        _navigationService  = navigationService;
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Group = group;
        State = state;
    }

    [RelayCommand]
    private void Remove()
    {
        if (State != PersonState.Remove) return;
        _databaseService.DeletePersonWith(Id);
        _navigationService.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void RemoveFromGroup()
    {
        if (State != PersonState.RemoveableFromGroup) return;

        _databaseService.DeletePersonFromGroup(Id, Group.Id);
        Group.Refresh();
    }
    
    [RelayCommand]
    private void AddToGroup()
    {
        if (State != PersonState.Add) return;
        
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
}