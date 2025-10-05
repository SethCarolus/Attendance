using Attendance.Services.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    [ObservableProperty]
    private ViewModelBase _currentViewModel;
    public MainWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;

        _navigationService.CurrentViewModelChanged += (s, e) =>
        {
            CurrentViewModel = e;
        };
        _navigationService.NavigateTo<GroupsViewModel>();
    }
}
