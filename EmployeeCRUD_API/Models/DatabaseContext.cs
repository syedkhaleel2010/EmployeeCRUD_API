using Microsoft.EntityFrameworkCore;

namespace EmployeeCRUD_API.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<EmployeeInfo> EmployeeInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfo");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.Password).HasMaxLength(20).IsUnicode(false);
               
            });

            modelBuilder.Entity<EmployeeInfo>(entity =>
            {
                entity.ToTable("EmployeeInfo");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.EmployeeId).IsUnicode(false);
                entity.Property(e => e.Name).IsUnicode(false);
                entity.Property(e => e.Gender).IsUnicode(false);
                entity.Property(e => e.JobDescription).IsUnicode(false);
                entity.Property(e => e.Salary).IsUnicode(false);

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
