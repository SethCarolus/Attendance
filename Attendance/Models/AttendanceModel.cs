using System;

namespace Attendance.Models;

public class AttendanceModel
{
    public AttendanceModel(int id, PersonModel person, SessionModel session)
    {
        Id = id;
        Person = person;
        Session = session;
    }
    
    public AttendanceModel(PersonModel person, SessionModel session)
    {
        Person = person;
        Session = session;
    }

    public int Id { get; set; }
    public PersonModel Person { get; set; }
    
    public SessionModel Session { get; set; }

    public AttendanceModel()
    {
    }
    
}