using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class EditGroupViewModel : ViewModelBase, IParameterReceiver<EditGroupParameters>
{
    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;

    [ObservableProperty]
    GroupViewModel _group;

    public EditGroupViewModel()
    {
    }

    public EditGroupViewModel(IAppContext appContext)
    {
        _navigation = appContext.NavigationService;
        _database = appContext.DatabaseService;
    }


    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void Edit()
    {
        var g = new GroupModel(Group.Id, Group.Name, Group.Description);
        _database.EditGroup(g);
        _navigation.NavigateTo<GroupsViewModel>();
    }

    public void ReceiveParameter(EditGroupParameters parameters)
    {
        Group = parameters.Group;
    }
}
