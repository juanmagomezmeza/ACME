using ACME.SchoolManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACME.SchoolManagement.Persistence.Contexts;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().HasKey(s => s.StudentID);
        modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
        modelBuilder.Entity<Enrollment>().HasKey(c => new { c.CourseID, c.StudentID });

        modelBuilder.Entity<Enrollment>()
        .HasOne(e => e.Student)
        .WithMany(s => s.Enrollments)
        .HasForeignKey(e => e.StudentID);

        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(c => c.Enrollments)
            .HasForeignKey(e => e.CourseID);
    }
}
