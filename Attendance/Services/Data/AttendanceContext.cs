using System;
using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance;

public class AttendanceContext: DbContext
{
    public DbSet<PersonModel> People { get; set; }
    public DbSet<GroupModel> Groups { get; set; }
    
    public string DbPath { get; }

    public AttendanceContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "attendance.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options) 
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<GroupModel>()
            .HasMany(g => g.People)
            .WithMany(p => p.Groups)
            .UsingEntity(j => j.ToTable("GroupPeople"));
    }
}