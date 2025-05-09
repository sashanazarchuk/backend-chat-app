using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Presentation.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository<ChatMessage> service;
        private readonly ITextAnalyticsService<Sentiment> textService;


        // Constructor to inject repository and sentiment analysis service
        public ChatHub(IChatMessageRepository<ChatMessage> service, ITextAnalyticsService<Sentiment> textService)
        {
            this.service = service;
            this.textService = textService;
        }


        // Method for processing and broadcasting incoming chat messages
        public async Task SendMessage(string user, string message)
        {
            // Analyze the sentiment of the message
            var sentiment = await textService.AnalyzeSentimentAsync(message);

            // Create a ChatMessage object with metadata and sentiment result
            var chatMessage = new ChatMessage
            {
                UserName = user,
                Message = message,
                Time = DateTime.UtcNow,
                Sentiment = sentiment
            };

            // Save the message to the database
            await service.SaveMessageAsync(chatMessage);


            // Broadcast the message and sentiment to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", new
            {
                chatMessage.UserName,
                chatMessage.Message,
                Time = chatMessage.Time,
                Sentiment = new
                {
                    chatMessage.Sentiment.Label,
                    chatMessage.Sentiment.Emoji
                }
            });
        }

    }
}
