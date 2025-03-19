using EducationCenter.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace EducationCenter.Data.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentGroup> StudentGroups { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
}
