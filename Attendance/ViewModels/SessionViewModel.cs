using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Attendance.ViewModels;

public partial class SessionViewModel: ViewModelBase
{    
    private readonly INavigationService _navigation;
    private readonly IDatabaseService  _database;
    private readonly IDialogService _dialog;
    
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
        string? description, IAppContext appContext)
    {
        _navigation = appContext.NavigationService;
        _database = appContext.DatabaseService;
        _dialog = appContext.DialogService;
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
            Attendances.Add(new(p, this, _database.WasPresent(p.Id, Id), appContext.DatabaseService)); // This passsing is bad!
        }
    }

    [RelayCommand]
    private void View()
    {
        _navigation.NavigateTo(this);
    }

    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<SessionsViewModel, SessionParameters>(new() {Group = Group});
    }

    [RelayCommand]
    private async Task RemoveAsync()
    {
        var displayName = string.IsNullOrWhiteSpace(Name) ? "Session" : Name;
        _dialog.Show<ConfirmationDialogViewModel>("Attendance", $"Are you sure you want to remove {displayName}?");
        var result = await _dialog.CurrentDialogViewModel.Task;
        if (!result) return;

        _database.DeleteSessionWith(Id);
        _navigation.NavigateTo<SessionsViewModel, SessionParameters>(new() {Group = Group});
    }

    [RelayCommand]
    private void Edit()
    {
        
    }

    
} 