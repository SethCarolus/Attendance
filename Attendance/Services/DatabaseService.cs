using System;
using System.Collections.Generic;
using System.Linq;
using Attendance.Models;
using Attendance.Services.Contracts;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Services;

public class DatabaseService : IDatabaseService
{
    private readonly AttendanceContext _context;

    public DatabaseService(AttendanceContext context)
    {
        _context = context;
    }
    
    public IList<GroupModel> GetGroups()
    {
        return _context.Groups.Include(g => g.People).ToList();
    }

    public GroupModel GetGroupWith(int id)
    {
        return _context.Groups.Include(g => g.People).First(g => g.Id == id);
    }

    public void AddGroup(GroupModel group)
    {
        _context.Add(group);
        _context.SaveChanges();
    }
    
    public void DeleteGroupWith(int id)
    {
        var group = _context.Groups.FirstOrDefault(g => g.Id == id);
        if (group == null) throw new Exception("Group not found");
        
        _context.Groups.Remove(group);
        _context.SaveChanges();
    }
    public void AddPerson(PersonModel person)
    {
        _context.People.Add(person);
        _context.SaveChanges();
    }

    public PersonModel GetPersonWith(int id)
    {
        return 
            _context
                .People
                .Include(p => p.Groups)
                .Single(p => p.Id ==  id);
    }

    public void DeletePersonWith(int id)
    {
        var person = _context.People.FirstOrDefault(p => p.Id == id);
        if (person == null) throw  new Exception("Person not found");
        
        _context.People.Remove(person);
        _context.SaveChanges();
    }

    public IList<PersonModel> GetPeople()
    {
        return _context.People.ToList();
    }

    public IList<PersonModel> GetPeopleInGroupWith(int id)
    {
        return _context.Groups.Include(g => g.People).First(g => g.Id == id).People.ToList();
    }

    public void DeletePersonFromGroup(int personId, int groupId)
    {
        var person = _context.People.Include(p => p.Groups).Single(p => p.Id == personId);
        var group = _context.Groups.Include(g => g.People).Single(g => g.Id == groupId);
        person.Groups!.Remove(group);
        _context.SaveChanges();
    }

    public void EditPerson(PersonModel person)
    {
        var p = _context.People.FirstOrDefault(_p => _p.Id == person.Id);
        
        p.FirstName = person.FirstName;
        p.LastName = person.LastName;
        _context.SaveChanges();
    }

    public void AddSession(SessionModel session)
    {
        ArgumentNullException.ThrowIfNull(session);
        session.Group = null;
        _context.Sessions.Add(session);
        _context.SaveChanges();
    }

    public SessionModel GetSessionWith(int id)
    {
        return 
            _context.Sessions
            .Include( s =>  s.Group)
            .ThenInclude(g => g.People)
            .ThenInclude(p => p.Groups)
            .Single(s => s.Id == id);
    }

    public IList<SessionModel> GetSessionsForGroupWith(int id)
    {
        return _context.Sessions
               .Include(s => s.Group)
               .ThenInclude(g => g.People)
               .Where(s => s.Group.Id == id).ToList();
    }

    public void DeleteSessionWith(int id)
    {
        _context.Sessions.Remove(_context.Sessions.Single(s => s.Id == id));
        _context.SaveChanges();
    }

    public void DeleteAttendance(int personId, int sessionId)
    {
        _context.Attendances.Remove(_context.Attendances.Single(a => a.Person.Id == personId && a.Session.Id == sessionId));
        _context.SaveChanges();
    }

    public void AddAttendance(int  personId, int sessionId)
    {
        var person = GetPersonWith(personId);
        var session = GetSessionWith(sessionId);

        _context.Attendances.Add(new(person, session));
        _context.SaveChanges();
    }

    public bool WasPresent(int personId, int sessionId)
    {
        return _context
                .Attendances
                .Include(a => a.Person)
                .Include(a => a.Session)
                .SingleOrDefault(a => a.Person.Id == personId && a.Session.Id == sessionId) != null;
    }

    public void EditSession(SessionModel session)
    {
        var s = _context.Sessions.FirstOrDefault(_s => _s.Id == session.Id);
        s.Name = session.Name;
        s.Description = session.Description;
        s.Start = session.Start;
        s.End = session.End;
        s.Date = session.Date;
        _context.SaveChanges();
    }
}