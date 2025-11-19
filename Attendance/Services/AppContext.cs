using Attendance.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Services
{
    public class AppContext: IAppContext
    {
        private readonly IDatabaseService _databaseService;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;

        public AppContext(IDatabaseService databaseService, IDialogService dialogService, INavigationService navigationService)
        {
            _databaseService = databaseService;
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public IDatabaseService DatabaseService => _databaseService;

        public IDialogService DialogService => _dialogService;

        public INavigationService NavigationService => _navigationService;
    }
}
