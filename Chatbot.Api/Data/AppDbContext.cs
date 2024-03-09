using Chatbot.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>((e) =>
            {
                e.HasKey(e => e.Id);

                e.HasMany(v => v.Messages)
                .WithOne(v => v.Chat)
                .HasForeignKey(v => v.ChatId);

                e.HasOne(v => v.User)
                .WithMany(v => v.Chats)
                .HasForeignKey(v => v.UserId);
            });

            modelBuilder.Entity<User>((e) =>
            {
                e.HasKey(e => e.Id);
            });
            modelBuilder.Entity<ChatMessage>((e) =>
            {
                e.HasKey(e => e.Id);
            });
        }
    }
}
