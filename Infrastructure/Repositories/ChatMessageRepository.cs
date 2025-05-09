using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository<ChatMessage>
    {
        private readonly ChatDbContext context;
        public ChatMessageRepository(ChatDbContext context)
        {
            this.context = context;
        }


        // Retrieves all chat messages with their associated sentiment, ordered by time
        public async Task<List<ChatMessage>> GetAllMessagesAsync()
        {
            return await context.Messages
                .Include(m => m.Sentiment)
                .OrderBy(m => m.Time)
                .ToListAsync();
        }

        // Saves a new chat message to the database
        public async Task SaveMessageAsync(ChatMessage message)
        {
             context.Messages.Add(message);
            await context.SaveChangesAsync();
        }

        // Deletes all chat messages from the database
        public async Task ClearHistory()
        {
            var message = await context.Messages.ToListAsync();

            context.Messages.RemoveRange(message);
            await context.SaveChangesAsync();
        }
    }
}
