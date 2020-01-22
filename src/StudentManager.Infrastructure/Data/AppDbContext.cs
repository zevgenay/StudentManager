using Microsoft.EntityFrameworkCore;
using StudentManager.Core.Entities;

namespace StudentManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasKey(s => s.Id);
            modelBuilder.Entity<Student>().Property(s => s.FirstName).HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.LastName).HasMaxLength(40).IsRequired();
            modelBuilder.Entity<Student>().Property(s => s.MiddleName).HasMaxLength(60);
            modelBuilder.Entity<Student>().Property(s => s.NickName).HasMaxLength(16);
            modelBuilder.Entity<Student>().HasAlternateKey(s => s.NickName);

            modelBuilder.Entity<Group>().HasKey(g => g.Id);
            modelBuilder.Entity<Group>().Property(g => g.Name).HasMaxLength(25).IsRequired();

            modelBuilder.Entity<StudentGroup>()
                .HasKey(t => new { t.StudentId, t.GroupId });

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Student)
                .WithMany(s => s.StudentGroups)
                .HasForeignKey(sg => sg.StudentId);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Group)
                .WithMany(g => g.StudentGroups)
                .HasForeignKey(sg => sg.GroupId);
        }
    }
}
