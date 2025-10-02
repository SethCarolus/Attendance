using System;
using System.Collections.ObjectModel;
using System.Linq;
using Attendance.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class GroupViewModel : ViewModelBase
{
    private MainWindowViewModel _mainWindowViewModel;
    
    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]
    private string _firstName;
    
    [ObservableProperty]
    private string _lastName;
    
    public GroupViewModel(GroupModel group, MainWindowViewModel mainWindowViewModel)
    {
        ArgumentNullException.ThrowIfNull(group);
        _mainWindowViewModel = mainWindowViewModel ?? throw new ArgumentNullException(nameof(mainWindowViewModel));
        
        Id = group.Id!.Value;
        Name = group.Name;
        Description = group.Description;
        People = new();

        foreach (var p in group.People)
        {
            People.Add(new(p.Id, p.FirstName,  p.LastName, this));
        }
    }
    
    [RelayCommand]
    private void View()
    {
        _mainWindowViewModel.CurrentViewModel = this;
    }

    [RelayCommand]
    private void Remove()
    {
        var context = new AttendanceContext();

        var group = context.Groups.FirstOrDefault(g => g.Id == Id);
        
        if (group == null) return;
        
        context.Groups.Remove(group);
        context.SaveChanges();
        _mainWindowViewModel.CurrentViewModel = new GroupsViewModel(_mainWindowViewModel);
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
        var group = context.Groups.Include(g => g.People).First(g => g.Id == Id);
        person.Groups.Add(group);
        
        context.People.Add(person);
        context.SaveChanges();
        
        Refresh();
    }

    public void Refresh()
    {
        var context = new AttendanceContext();
        var group = context.Groups.Include(g => g.People).First(g => g.Id == Id);

        People = new();
        foreach (var p in group.People)
        {
            People.Add(new(p.Id, p.FirstName, p.LastName, this));
        }  
    }
}