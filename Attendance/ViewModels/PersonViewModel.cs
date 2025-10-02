using System;
using System.Linq;
using Attendance.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class PersonViewModel : ViewModelBase
{

    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _firstName;
    
    [ObservableProperty]
    private string _lastName;
    
    [ObservableProperty]
    private GroupViewModel _group;
    
    public PersonViewModel(int id, string firstName, string lastName, GroupViewModel group)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Group = group;
    }

    [RelayCommand]
    private void Remove()
    {
        if (Group == null) throw new ArgumentNullException(nameof(Group));
        
        var context = new AttendanceContext();
        var person = context.People.FirstOrDefault(p => p.Id == Id);
        if (person == null) return;
        
        context.People.Remove(person);
        
        context.SaveChanges();
        
        Group.Refresh();
    }
}