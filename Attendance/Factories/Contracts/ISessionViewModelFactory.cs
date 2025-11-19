using Attendance.Services.Contracts;
using Attendance.ViewModels;
using System;

namespace Attendance.Factories.Contracts
{
    public interface ISessionViewModelFactory
    {
        public SessionViewModel Create(int id, GroupViewModel group, DateOnly date, TimeOnly? start, TimeOnly? end, string? name,
        string? description);
    }
}
