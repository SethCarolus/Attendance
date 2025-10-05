using System;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Attendance.Services;

public class NavigationService: INavigationService
{
    private readonly IServiceProvider _serviceProvider;
    private ViewModelBase _currentViewModel;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void NavigateTo<T>() where T : ViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();
        NavigateTo(viewModel);
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        _currentViewModel = viewModel;
        CurrentViewModelChanged?.Invoke(this, viewModel);
    }

    public event EventHandler<ViewModelBase>? CurrentViewModelChanged;

    public ViewModelBase CurrentViewModel => _currentViewModel;
}