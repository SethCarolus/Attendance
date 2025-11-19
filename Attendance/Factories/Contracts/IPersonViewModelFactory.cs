

using Attendance.Enums;
using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels;
using System.Collections.Generic;

namespace Attendance.Factories.Contracts
{
    public interface IPersonViewModelFactory
    {
        PersonViewModel Create(PersonModel person, GroupViewModel group);
        PersonViewModel Create(PersonModel person, GroupViewModel group, List<PersonState> states);

    }
}
