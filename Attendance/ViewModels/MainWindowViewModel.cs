using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _CurrentViewModel;
    public MainWindowViewModel()
    {
        _CurrentViewModel = new GroupsViewModel(this);
    }
}
