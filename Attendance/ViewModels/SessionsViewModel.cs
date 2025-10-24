using System;
using System.Collections.ObjectModel;
using System.Linq;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class SessionsViewModel: ViewModelBase, IParameterReceiver<SessionParameters>
{

    private readonly INavigationService _navigationService;
    private readonly IDatabaseService _databaseService;
    private GroupViewModel _groupViewModel;
    
    [ObservableProperty]
    private  ObservableCollection<SessionViewModel> _sessions;

    [ObservableProperty]
    private string? _name;
    
    [ObservableProperty]
    private string? _description;

    [ObservableProperty]
    private DateTime _selectedDate =  DateTime.Now;

    [ObservableProperty]    
    private TimeSpan? _start;
    
    [ObservableProperty]   
    private TimeSpan? _end;
        
    
    public SessionsViewModel(INavigationService navigationService, IDatabaseService databaseService)
    {
        _navigationService  =  navigationService;
        _databaseService = databaseService;
    }

    public SessionsViewModel()
    {
    }
    
    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void AddSession()
    {
        var group = new GroupModel(_groupViewModel.Id, _groupViewModel.Name, _groupViewModel.Description);
        var session = new SessionModel(group, Name, Description, DateOnly.FromDateTime(SelectedDate), null, null);
        
        if (Start.HasValue && End.HasValue)
        {
            session.Start = TimeOnly.FromTimeSpan(Start.Value);
            session.End = TimeOnly.FromTimeSpan(End.Value);
        }
        _databaseService.AddSession(session);
        Refresh();
    }

    [RelayCommand]
    void Refresh()
    {
        Sessions = new();
        foreach (var s in 
                 _databaseService.GetSessionsForGroupWith(_groupViewModel.Id)
                     .Where( s => s.Date == DateOnly.FromDateTime(SelectedDate)))
        {
            Sessions.Add(
                new(
                    s.Id, 
                    _groupViewModel, 
                    s.Date ,
                    s.Start, 
                    s.End,
                    s.Name, 
                    s.Description, 
                    _navigationService, 
                    _databaseService));
        }
    }

    partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
    {
        Refresh();
    }

    public void ReceiveParameter(SessionParameters parameters)
    {
        _groupViewModel = parameters.Group;
        Refresh();
    }
}