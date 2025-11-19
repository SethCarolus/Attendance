using System;
using System.Collections.ObjectModel;
using System.Linq;
using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class SessionsViewModel: ViewModelBase, IParameterReceiver<SessionsParameters>
{

    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;
    private readonly ISessionViewModelFactory _sessionViewModelFactory;
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
        
    
    public SessionsViewModel(IAppContext appContext, ISessionViewModelFactory sessionViewModelFactory)
    {
        _navigation  =  appContext.NavigationService;
        _database = appContext.DatabaseService;
        _sessionViewModelFactory = sessionViewModelFactory;
    }

    public SessionsViewModel()
    {
    }
    
    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<GroupsViewModel>();
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
        _database.AddSession(session);
        Refresh();
    }

    [RelayCommand]
    void Refresh()
    {
        Sessions = new();
        foreach (var s in 
                 _database.GetSessionsForGroupWith(_groupViewModel.Id)
                     .Where( s => s.Date == DateOnly.FromDateTime(SelectedDate)))
        {
            Sessions.Add(
                _sessionViewModelFactory.Create(s.Id,
                    _groupViewModel,
                    s.Date,
                    s.Start,
                    s.End,
                    s.Name,
                    s.Description
                    ));
        }
    }

    partial void OnSelectedDateChanged(DateTime oldValue, DateTime newValue)
    {
        Refresh();
    }

    public void ReceiveParameter(SessionsParameters parameters)
    {
        _groupViewModel = parameters.Group;
        Refresh();
    }
}