using AdultsWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdultsWebAPI.Persistence
{
    public class AdultsContext:DbContext
    {
        public DbSet<Adult> Adults { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // name of database
            optionsBuilder.UseSqlite("Data Source = Adult.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(user => new
            {
                user.UserName,
                user.Password
        
            });
        }
        
    }
}