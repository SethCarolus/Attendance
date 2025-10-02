using System.Collections.ObjectModel;
using System.Linq;
using Attendance.Enums;
using Attendance.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class PeopleViewModel : ViewModelBase
{
    private MainWindowViewModel _mainWindowViewModel;
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]
    private string? _firstName;
    
    [ObservableProperty]
    private string? _lastName;

    public PeopleViewModel(MainWindowViewModel mainWindowViewModel)
    {
        _mainWindowViewModel = mainWindowViewModel;
        Refresh();
    }

    [RelayCommand]
    private void Back()
    {
        _mainWindowViewModel.CurrentViewModel = new GroupsViewModel(_mainWindowViewModel);
    }

    [RelayCommand]
    private void Add()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            return;
        }
        
        var context = new AttendanceContext();

        var person = new PersonModel(FirstName, LastName);
        
        context.People.Add(person);
        context.SaveChanges();

        FirstName = "";
        LastName = "";
        Refresh();
    }

    private void Refresh()
    {
        var context = new AttendanceContext();
        
        People = new();
        foreach (var person in context.People)
        {
            People.Add(new(person.Id, person.FirstName, person.LastName, null, PersonState.Remove));
        }       
    }
}