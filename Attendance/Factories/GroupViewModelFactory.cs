using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Attendance.Factories
{
    public class GroupViewModelFactory : IGroupViewModelFactory
    {
        private IServiceProvider _serviceProvider;
        public GroupViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        GroupViewModel IGroupViewModelFactory.Create(GroupModel group)
        {
            return new GroupViewModel(group, _serviceProvider.GetRequiredService<IAppContext>(), _serviceProvider.GetRequiredService<IPersonViewModelFactory>());
        }
    }
}
