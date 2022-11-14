using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TwoeterApi.Model.Entity;

namespace TwoeterApi
{
    public class TwoeterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql("server=localhost;user=root;database=twoeter_db",
                        ServerVersion.Parse("5.7.33-mysql"))
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

                entity.HasMany(u => u.Followers)
                    .WithOne(f => f.Following)
                    .HasForeignKey(f => f.FollowingId);

                entity.HasMany(u => u.Following)
                    .WithOne(f => f.Follower)
                    .HasForeignKey(f => f.FollowerId);
            });
        }
    }
}
