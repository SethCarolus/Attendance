using System.Collections.ObjectModel;
using System.Runtime.InteropServices.JavaScript;
using Attendance.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;

namespace Attendance.ViewModels;

public partial class GroupsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<GroupViewModel> _groups;
    
    private AttendanceContext _attendanceContext;

    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;

    public GroupsViewModel()
    {
        _attendanceContext = new AttendanceContext();

        LoadData();
    }

    private void LoadData()
    {
        Groups = new();
        foreach (var group in _attendanceContext.Groups.Include(groupModel => groupModel.People))
        {
            var gvm = new GroupViewModel(group.Name, group.Description, new());
            
            foreach (var person in group.People)
            {
                gvm.People.Add(new(person.Id, person.FirstName, person.LastName));
            }
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
        _attendanceContext.Add(new GroupModel(Name, Description));
        _attendanceContext.SaveChanges();
        LoadData();

        Name = "";
        Description = "";
    }
}