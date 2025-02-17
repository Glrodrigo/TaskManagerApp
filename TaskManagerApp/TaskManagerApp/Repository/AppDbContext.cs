using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Domain;

namespace TaskManagerApp.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskManagerBase> Tasks { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TaskManagerBase>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<TaskManagerBase>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            modelBuilder.Entity<TaskManagerBase>()
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
