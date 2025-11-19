using Attendance.ViewModels.Dialogs;
using System;

namespace Attendance.Services.Contracts
{
    public interface IDialogService
    {
        public void Show<T>(string title, string content) where T: DialogViewModel;
        public DialogViewModel CurrentDialogViewModel { get; }
        public event EventHandler<DialogViewModel> CurrentDialogViewModelChanged;
    }
}
