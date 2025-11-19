using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialog;

    [ObservableProperty]
    private ViewModelBase _currentViewModel;

    [ObservableProperty]
    private DialogViewModel _currentDialog;

    public MainWindowViewModel(INavigationService navigationService, IDialogService dialogService)
    {
        _navigationService = navigationService;
        _dialog = dialogService;

        _navigationService.CurrentViewModelChanged += (s, e) =>
        {
            CurrentViewModel = e;
        };
        _navigationService.NavigateTo<GroupsViewModel>();

        _dialog.CurrentDialogViewModelChanged += (s, e) =>
        {
            CurrentDialog = e;
        };
    }

    public MainWindowViewModel()
    {
    }
}
