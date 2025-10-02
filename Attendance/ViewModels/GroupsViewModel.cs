using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using Attendance.Models;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class GroupsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<GroupViewModel> _groups;

    private MainWindowViewModel _mainWindowViewModel;

    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;

    public GroupsViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));

        LoadDataAsync();
    }

    private void LoadDataAsync()
    {
        Groups = new();

        var context = new AttendanceContext();
        
        var groups = context.Groups.Include(g => g.People).ToList();

        foreach (var group in groups)
        {
            var gvm = new GroupViewModel(group, _mainWindowViewModel);
            Groups.Add(gvm);
        }
        
    }

    [RelayCommand]
    private void Add()
    {
        if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(Description))
        {
            return;
        }
        
        var context = new AttendanceContext();
        context.Add(new GroupModel(null,Name, Description));
        context.SaveChanges();
        LoadDataAsync();

        Name = "";
        Description = "";
    }
}