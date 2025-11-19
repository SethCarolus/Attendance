using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Attendance.Services;
using Attendance.Services.Contracts;
using Avalonia.Markup.Xaml;
using Attendance.ViewModels;
using Attendance.Views;
using Microsoft.Extensions.DependencyInjection;
using Attendance.Factories.Contracts;
using Attendance.Factories;
using Attendance.ViewModels.Dialogs;

namespace Attendance;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var collection = new ServiceCollection();
        
        // Services
        collection.AddSingleton<AttendanceContext>();
        collection.AddSingleton<INavigationService, NavigationService>();
        collection.AddTransient<IDatabaseService, DatabaseService>();
        collection.AddSingleton<IDialogService, DialogService>();
        collection.AddTransient<IAppContext, AppContext>();

        // Factories
        collection.AddSingleton<IGroupViewModelFactory, GroupViewModelFactory>();
        collection.AddSingleton<IPersonViewModelFactory, PersonViewModelFactory>();
        collection.AddSingleton<ISessionViewModelFactory, SessionViewModelFactory>();


        // Dialogs

        collection.AddTransient<ConfirmationDialogViewModel>();


        // View Models
        collection.AddTransient<MainWindowViewModel>();
        collection.AddTransient<GroupsViewModel>();
        collection.AddTransient<PeopleViewModel>();
        collection.AddTransient<SessionsViewModel>();
        collection.AddTransient<EditPersonViewModel>();
        collection.AddTransient<EditSessionViewModel>();
        
        var services = collection.BuildServiceProvider();
        
        var vm = services.GetRequiredService<MainWindowViewModel>();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}