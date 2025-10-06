using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Attendance.Enums;
using Attendance.Models;
using Attendance.Services;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class GroupViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IDatabaseService _databaseService;
    
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]   
    private ObservableCollection<PersonViewModel> _allPeople;
    
    
    public GroupViewModel(GroupModel group, INavigationService navigationService, IDatabaseService databaseService)
    {
        ArgumentNullException.ThrowIfNull(group);
        
        _navigationService = navigationService;
        _databaseService = databaseService;

        Id = group.Id;
        Name = group.Name;
        Description = group.Description;
        
        People = new();
        foreach (var p in group.People)
        {
            People.Add(new(p.Id, p.FirstName,  p.LastName, this, PersonState.RemoveableFromGroup, databaseService, _navigationService));
        }
        
        var context =  new AttendanceContext();
        AllPeople = new();
        foreach (var p in context.People.Where(p => !group.People.Contains(p)))
        {
            AllPeople.Add(new(p.Id, p.FirstName,  p.LastName, this, PersonState.Add, databaseService, navigationService));       
        }
    }
    
    [RelayCommand]
    private void View()
    {
        _navigationService.NavigateTo(this);
    }

    [RelayCommand]
    private void Sessions()
    {
        _navigationService.NavigateTo<SessionsViewModel, SessionParameters>(new() {Group = this});
    }

    [RelayCommand]
    private void Remove()
    {
        _databaseService.DeleteGroupWith(Id);
        _navigationService.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo<GroupsViewModel>();
    }
    
    public void Refresh()
    {
        var group = _databaseService.GetGroupWith(Id);

        People = new();
        foreach (var p in group.People)
        {
            People.Add(new(p.Id, p.FirstName, p.LastName, this, PersonState.RemoveableFromGroup, _databaseService, _navigationService));
        }  
        
        AllPeople = new();
        foreach (var p in _databaseService.GetPeople().Where(p => !group.People.Contains(p)))
        {
            AllPeople.Add(new(p.Id, p.FirstName,  p.LastName, this, PersonState.Add, _databaseService, _navigationService));       
        }
    }
}