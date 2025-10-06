using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class SessionViewModel: ViewModelBase
{    
    private readonly INavigationService _navigationService;
    private readonly IDatabaseService  _databaseService;
    
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private GroupViewModel _group;
    
    [ObservableProperty]
    private DateOnly _date;
    
    [ObservableProperty]
    private TimeOnly? _start;
    
    [ObservableProperty]
    private TimeOnly? _end;
    
    [ObservableProperty]
    private string? _name;
    
    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private ObservableCollection<AttendanceViewModel> _attendances;

    public SessionViewModel()
    {
        
    }

    public SessionViewModel(int id, GroupViewModel group, DateOnly date, TimeOnly? start, TimeOnly? end, string? name,
        string? description, INavigationService navigationService, IDatabaseService databaseService)
    {
        _navigationService = navigationService;
        _databaseService = databaseService;
        Id = id;
        Group = group;
        Date = date;
        Start = start;
        End = end;
        Name = name;
        Description = description;
        Attendances = new();
        foreach (var p in Group.People)
        {
            Attendances.Add(new(p, this, _databaseService.WasPresent(p.Id, Id), databaseService));
        }
    }

    [RelayCommand]
    private void View()
    {
        _navigationService.NavigateTo(this);
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo<SessionsViewModel, SessionParameters>(new() {Group = Group});
    }

    [RelayCommand]
    private void Remove()
    {
        _databaseService.DeleteSessionWith(Id);
        _navigationService.NavigateTo<SessionsViewModel, SessionParameters>(new() {Group = Group});
    }

    [RelayCommand]
    private void Edit()
    {
        
    }

    
} 