using System;
using Attendance.ViewModels;

namespace Attendance.Services.Contracts;

public interface INavigationService
{
    void NavigateTo<T>() where T : ViewModelBase;
    void NavigateTo(ViewModelBase viewModel);
    event EventHandler<ViewModelBase> CurrentViewModelChanged;
    ViewModelBase CurrentViewModel { get; }
}