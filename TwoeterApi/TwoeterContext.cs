using Microsoft.EntityFrameworkCore;
using TwoeterApi.Model.Entity;

namespace TwoeterApi
{
    public class TwoeterContext : DbContext
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<Post>? Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;database=twoeter_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.33-mysql"))
                    .LogTo(Console.WriteLine, LogLevel.Warning);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasMany(u => u.Posts)
                    .WithOne(p => p.Author)
                    .HasForeignKey(p => p.AuthorId);
                
                entity.HasMany(u => u.DeletedPosts)
                    .WithOne(p => p.DeletedBy)
                    .HasForeignKey(p => p.DeletedById);
            });
        }
    }
}
