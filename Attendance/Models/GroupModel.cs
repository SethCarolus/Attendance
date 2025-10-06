using System.Collections.Generic;

namespace Attendance.Models;

public class GroupModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    public List<PersonModel> People { get; set; }
    
    public GroupModel(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        People = new();
    }
    
    public GroupModel(string name, string description)
    {
        Name = name;
        Description = description;
        People = new();
    }

    public GroupModel()
    {
        
    }
}