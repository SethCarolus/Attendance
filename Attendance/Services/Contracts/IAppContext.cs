using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Services.Contracts
{
    public interface IAppContext
    {
        public IDatabaseService DatabaseService { get; }
        public IDialogService DialogService { get; }
        public INavigationService NavigationService { get; }
    }
}
