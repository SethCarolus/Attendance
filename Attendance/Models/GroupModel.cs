using System.Collections.Generic;

namespace Attendance.Models;

public class GroupModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<PersonModel> People { get; set; }
    
    public GroupModel(string name, string description, List<PersonModel> people)
    {
        Name = name;
        Description = description;
        People = people;
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