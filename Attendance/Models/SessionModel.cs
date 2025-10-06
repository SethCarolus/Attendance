using System;

namespace Attendance.Models;

public class SessionModel
{
    public int Id { get; set; }
    
    public int GroupId { get; set; }
    public GroupModel Group { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly? Start { get; set; }
    public TimeOnly? End { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public SessionModel()
    {
        
    }
    
    public SessionModel(GroupModel group, string? name, string? description, DateOnly date, TimeOnly? start, TimeOnly? end)
    {
        Group = group;
        GroupId = Group.Id;
        Name = name;
        Description = description;
        Date = date;
        Start = start;
        End = end;
    }
}