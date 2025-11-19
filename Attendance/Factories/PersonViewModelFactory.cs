
using Attendance.Enums;
using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Attendance.Factories
{
    public class PersonViewModelFactory : IPersonViewModelFactory
    {

        private readonly IServiceProvider _serviceProvider;
        public PersonViewModelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        PersonViewModel IPersonViewModelFactory.Create(PersonModel person, GroupViewModel group)
        {
            return new PersonViewModel(person.Id, person.FirstName, person.LastName, group, [], _serviceProvider.GetRequiredService<IAppContext>());
        }

        PersonViewModel IPersonViewModelFactory.Create(PersonModel person, GroupViewModel group,List<PersonState> states)
        {
            return new PersonViewModel(person.Id, person.FirstName, person.LastName, group, states , _serviceProvider.GetRequiredService<IAppContext>());
        }
    }
}
