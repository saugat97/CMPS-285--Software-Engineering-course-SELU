using LionsFeed.Models;
using Microsoft.EntityFrameworkCore;

namespace LionsFeed.Data
{
    public class LionsContext : DbContext
    {
        public LionsContext(DbContextOptions<LionsContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Upvote> Upvotes { get; set; }
        public DbSet<Flag> Flags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Upvote>()
                .HasKey(t => new { t.PostId, t.UpvoterId });

            modelBuilder.Entity<Upvote>()
               .HasOne(a => a.Post)
               .WithMany()
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasKey(t => new { t.Id });

            modelBuilder.Entity<Comment>()
              .HasOne(a => a.Post)
              .WithMany(b => b.Comments)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Flag>()
                .HasOne(a => a.Post)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserNotification>()
     .HasKey(t => new { t.UserId, t.NotificationId });

            modelBuilder.Entity<UserNotification>()
                .HasOne(a => a.User)
                .WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
