using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            // Registers infrastructure services for dependency injection
            services.AddScoped<IChatMessageRepository<ChatMessage>, ChatMessageRepository>();
            services.AddSingleton<ITextAnalyticsService<Sentiment>, TextAnalyticsService>();
        }
    }
}