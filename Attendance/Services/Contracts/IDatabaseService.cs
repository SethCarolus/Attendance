using System.Collections.Generic;
using Attendance.Models;

namespace Attendance.Services.Contracts;

public interface IDatabaseService
{
    public IList<GroupModel> GetGroups();
    public GroupModel GetGroupWith(int id);
    public void AddGroup(GroupModel group);
    
    public void DeleteGroupWith(int id);
    public void EditGroup(GroupModel group);
    
    public void AddPerson(PersonModel person);
    public void DeletePersonWith(int id);
    public IList<PersonModel> GetPeople();
    public IList<PersonModel> GetPeopleInGroupWith(int id);
    public void DeletePersonFromGroup(int personId, int groupId);

    public void EditPerson(PersonModel person);
    
    public void AddSession(SessionModel session);

    public IList<SessionModel> GetSessionsForGroupWith(int id);
    public void DeleteSessionWith(int id);
    public void DeleteAttendance(int personId, int sessionId);
    public void AddAttendance(int  personId, int sessionId);
    public bool WasPresent(int personId, int sessionId);
    public void EditSession(SessionModel session);
}