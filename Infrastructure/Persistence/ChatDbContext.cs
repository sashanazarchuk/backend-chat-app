using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ChatDbContext: DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base (options) { }

        // Configures the relationships and behaviors between the ChatMessage and Sentiment entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configures a one-to-one relationship between ChatMessage and Sentiment using Fluent API
            modelBuilder.Entity<ChatMessage>()
            .HasOne(m => m.Sentiment)
            .WithOne()
            .HasForeignKey<Sentiment>(s => s.ChatMessageId)
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<Sentiment> Sentiments { get; set; }
    }
}