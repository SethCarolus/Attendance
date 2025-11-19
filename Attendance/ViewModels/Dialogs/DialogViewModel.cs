using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ViewModels.Dialogs
{
    public partial class DialogViewModel: ViewModelBase
    {
        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _message;

        [ObservableProperty]
        private bool _isOpen = false;

        protected TaskCompletionSource<bool> _tcs = new TaskCompletionSource<bool>();

        public Task<bool> Task => _tcs.Task;

        public void Show()
        {
            IsOpen = true;
        }

        public void Close()
        {
            IsOpen = false;
        }

    }
}
