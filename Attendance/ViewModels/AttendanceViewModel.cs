using System;
using Attendance.Models;
using Attendance.Services.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class AttendanceViewModel: ViewModelBase
 {
     private readonly SessionViewModel _session;
     private readonly IDatabaseService _database;
     
     [ObservableProperty]
     private PersonViewModel _person;
     
     [ObservableProperty]
     private bool _present;
     public AttendanceViewModel()
     {
     }
     
     public AttendanceViewModel(PersonViewModel person, SessionViewModel session ,bool present, IDatabaseService  databaseService)
     {
         Person = person;
         _session = session;
         _present = present;
         _database = databaseService;
     }

     [RelayCommand]
     private void Handle()
     {
        if (!Present)
        {
            _database.DeleteAttendance(Person.Id, _session.Id);
        }
        else
        {
            _database.AddAttendance(Person.Id,  _session.Id);
        }
     }
 }