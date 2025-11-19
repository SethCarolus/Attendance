using Attendance.Services.Contracts;
using Attendance.ViewModels.Dialogs;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Attendance.Services
{
    public class DialogService: IDialogService
    {

        private IServiceProvider _serviceProvider;
        private DialogViewModel _currentDialogViewmModel;

        public event EventHandler<DialogViewModel> CurrentDialogViewModelChanged;

        public DialogViewModel CurrentDialogViewModel => _currentDialogViewmModel;

        public DialogService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Show<T>(string title, string message) where T : DialogViewModel
        {
            _currentDialogViewmModel = _serviceProvider.GetRequiredService<T>();
            _currentDialogViewmModel.Title = title;
            _currentDialogViewmModel.Message = message;
            _currentDialogViewmModel?.Show();
            CurrentDialogViewModelChanged.Invoke(this, CurrentDialogViewModel);
        }
    }
}
