using Microsoft.EntityFrameworkCore;
namespace SwayApi.Entities
{
    public class SwayDbContext : DbContext
    {
        private string _connectionString =
              "Server=localhost;Port=5432;Database=SwayDb;Username=postgres;Password=root";
        public DbSet<User> Users { get; set; } 
        public DbSet<Role> Roles { get; set; }
        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>()
               .Property(u => u.Name)
               .IsRequired();

            modelBuilder.Entity<ToDoTask>()
               .Property(t => t.Title)
               .IsRequired();

            modelBuilder.Entity<Role>()
                .HasData(
                new Role()
                {
                    Id = 1,
                    Name = "Employee"
                },
                new Role()
                {
                    Id = 2,
                    Name = "Manager"
                },
                new Role()
                {
                    Id = 3,
                    Name = "Admin"
                });

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

           
        }
     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
              => optionsBuilder.UseNpgsql(_connectionString);

    }
}
