using Application.Interfaces;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class DbServiceExtension
    {
        public static void ConfiguredDbContext(this IServiceCollection services, IConfiguration configuration)
        {

            // Configures the DbContext to use Azure SQL Server connection string from configuration
            services.AddDbContext<ChatDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("AzureSqlConnection"));
            });
        }
    }
}
