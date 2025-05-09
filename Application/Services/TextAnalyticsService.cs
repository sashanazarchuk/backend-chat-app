using Application.Interfaces;
using Azure;
using Azure.AI.TextAnalytics;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Application.Services
{
    public class TextAnalyticsService : ITextAnalyticsService<Sentiment>
    {
        private readonly TextAnalyticsClient client;

        // Constructor to initialize TextAnalyticsService
        public TextAnalyticsService(IConfiguration config)
        {
            // Read the endpoint and API key from the configuration
            var endpoint = new Uri(config["AzureCognitiveServices:Endpoint"]);
            var key = new AzureKeyCredential(config["AzureCognitiveServices:ApiKey"]);

            // Create a client to interact with the Text Analytics API
            client = new TextAnalyticsClient(endpoint, key);
        }


        // Asynchronous method for analyzing text sentiment
        public async Task<Sentiment> AnalyzeSentimentAsync(string text)
        {
            // Call the API for sentiment analysis
            var response = await client.AnalyzeSentimentAsync(text);

            // Convert the result to a stream and set the mood tag
            var sentimentLabel = response.Value.Sentiment.ToString().ToLowerInvariant();


            // Determine the appropriate emoji depending on the mood
            var emoji = sentimentLabel switch
            {
                "positive" => "😊",
                "neutral" => "😐",
                "negative" => "😞",
                _ => "🤔"
            };

            // Return the result with the mood label and emoji
            return new Sentiment
            {
                Label = sentimentLabel,
                Emoji = emoji
            };
        }
    }
}
