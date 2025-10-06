using System;
using System.Collections.Generic;
using Attendance.ViewModels;

namespace Attendance.Services.Contracts;

public interface INavigationService
{
    void NavigateTo<T>() where T : ViewModelBase;
    public void NavigateTo<T, TParam>(TParam parameters) where T : ViewModelBase;
    void NavigateTo(ViewModelBase viewModel);
    event EventHandler<ViewModelBase> CurrentViewModelChanged;
    ViewModelBase? CurrentViewModel { get; }
}