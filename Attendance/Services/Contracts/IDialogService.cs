using Attendance.ViewModels.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Services.Contracts
{
    public interface IDialogService
    {
        public void Show<T>(string title, string content) where T: DialogViewModel;
        public DialogViewModel CurrentDialogViewModel { get; }
        public event EventHandler<DialogViewModel> CurrentDialogViewModelChanged;
    }
}
