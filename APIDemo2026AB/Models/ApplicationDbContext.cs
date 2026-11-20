using Microsoft.EntityFrameworkCore;

namespace APIDemo2026AB.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
           DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>()
                .HasKey(c => c.IdCourse);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .HasMaxLength(45);

            modelBuilder.Entity<Grade>()
                .HasKey(g => g.IdGrade);

            modelBuilder.Entity<Grade>()
                .Property(g => g.Name)
                .HasMaxLength(45);

            modelBuilder.Entity<Grade>()
                .Property(g => g.Description)
                .HasMaxLength(200);

            modelBuilder.Entity<Student>()
                .HasKey(s => s.IdStudent);

            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .HasMaxLength(45);

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .HasMaxLength(45);

            modelBuilder.Entity<Student>()
                .Property(s => s.Phone)
                .HasMaxLength(45);

            modelBuilder.Entity<Student>()
                .Property(s => s.Email)
                .HasMaxLength(45);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Grade)
                .WithMany()
                .HasForeignKey(s => s.GradeIdGrade);

            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.IdEnrollment);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentIdStudent);

            modelBuilder.Entity<Enrollment>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseIdCourse);
        }
    }
}
