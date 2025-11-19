using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.ViewModels.Dialogs
{
    public partial class ConfirmationDialogViewModel: DialogViewModel
    {

        [ObservableProperty]
        private string _confirmText = "Yes";

        [ObservableProperty]
        private string _cancelText = "No";

        [ObservableProperty]
        private bool _result;

        public ConfirmationDialogViewModel()
        { 
        }

        public ConfirmationDialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        [RelayCommand]
        private void Confirm()
        {
            Result = true;
            _tcs.TrySetResult(Result);
            Close();
        }


        [RelayCommand]
        private void Cancel()
        {
            Result = false;
            _tcs.TrySetResult(Result);
            Close();
        }

    }
}
