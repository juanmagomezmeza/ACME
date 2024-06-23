using ACME.SchoolManagement.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ACME.SchoolManagement.Persistence.Contexts;

public class SchoolContext : DbContext
{
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();

    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.StudentID);
            entity.Property(s => s.StudentID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(c => c.CourseID);
            entity.Property(c => c.CourseID).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.CourseID, e.StudentID });

            entity.HasOne(e => e.Student)
                  .WithMany(s => s.Enrollments)
                  .HasForeignKey(e => e.StudentID)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Course)
                  .WithMany(c => c.Enrollments)
                  .HasForeignKey(e => e.CourseID)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
