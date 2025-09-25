using System.Collections.ObjectModel;
using Attendance.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class GroupsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<GroupModel> _groups;

    public GroupsViewModel()
    {
        _groups = new();
    }
}