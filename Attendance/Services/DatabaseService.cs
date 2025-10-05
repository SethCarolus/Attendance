using System;
using System.Collections.Generic;
using System.Linq;
using Attendance.Models;
using Attendance.Services.Contracts;
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
}