using Attendance.Factories.Contracts;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Attendance.Factories;

public class SessionViewModelFactory : ISessionViewModelFactory
{
    private readonly IServiceProvider _serviceProvider;
    public SessionViewModelFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    SessionViewModel ISessionViewModelFactory.Create(int id, GroupViewModel group, DateOnly date, TimeOnly? start, TimeOnly? end, string? name, string? description)
    {
        return new(id, group, date, start, end, name, description, _serviceProvider.GetRequiredService<IAppContext>());
    }
}
