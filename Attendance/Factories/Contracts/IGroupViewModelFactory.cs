using Attendance.Models;
using Attendance.ViewModels;

namespace Attendance.Factories.Contracts
{
    public interface IGroupViewModelFactory
    {

        public GroupViewModel Create(GroupModel group);
    }
}
