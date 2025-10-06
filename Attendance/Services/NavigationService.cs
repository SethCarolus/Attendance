using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using Attendance.ViewModels.Contracts;
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

    public void NavigateTo<T, TParam>(TParam parameters) where T : ViewModelBase
    {
        var viewModel = _serviceProvider.GetRequiredService<T>();

        if (viewModel is IParameterReceiver<TParam> parameterReceiver)
        {
            parameterReceiver.ReceiveParameter(parameters);
        }
        else
        {
            throw new InvalidOperationException($"ViewModel {typeof(T).Name} does not implement IParameterReceiver<{typeof(TParam).Name}>");
        }
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