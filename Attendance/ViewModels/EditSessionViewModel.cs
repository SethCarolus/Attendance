using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Contracts;
using Attendance.ViewModels.Dialogs;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;

namespace Attendance.ViewModels;

public partial class EditSessionViewModel: ViewModelBase, IParameterReceiver<EditSessionParameters>
{
    private readonly INavigationService _navigation;
    private readonly IDialogService _dialog;
    private readonly IDatabaseService _database;

    [ObservableProperty]
    private SessionViewModel _session;


    [ObservableProperty]
    private TimeSpan? _start;

    [ObservableProperty]
    private TimeSpan? _end;

    public EditSessionViewModel()
    {
    }

    public EditSessionViewModel(IAppContext context)
    {
        _navigation = context.NavigationService;
        _dialog = context.DialogService;
        _database = context.DatabaseService;
    }

    public void ReceiveParameter(EditSessionParameters parameters)
    {
        Session = parameters.Session;
        Start = Session.Start.GetValueOrDefault().ToTimeSpan();
        End = Session.End.GetValueOrDefault().ToTimeSpan();
    }

    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<SessionsViewModel, SessionsParameters>(new() { Group = Session.Group });
    }

    [RelayCommand]
    public async Task EditAsync()
    {
        _dialog.Show<ConfirmationDialogViewModel>("Attendance", "Are you sure you want to edit the session?");

        var result = await _dialog.CurrentDialogViewModel.Task;

        if (!result) return;
        var session = new SessionModel(Session.Name, Session.Description, Session.Date, TimeOnly.FromTimeSpan(Start.GetValueOrDefault()), TimeOnly.FromTimeSpan(End.GetValueOrDefault()));
        session.Id = Session.Id;
        _database.EditSession(session);

        _navigation.NavigateTo<SessionsViewModel, SessionsParameters>(new() { Group = Session.Group});
    }
}
