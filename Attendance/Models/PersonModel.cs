using System.Collections.Generic;

namespace Attendance.Models;

public class PersonModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<GroupModel> Groups { get; set; }
    
    public PersonModel(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        Groups = new();
    }

    public PersonModel()
    {
        
    }
}