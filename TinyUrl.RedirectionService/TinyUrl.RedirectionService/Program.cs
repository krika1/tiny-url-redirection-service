
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TinyUrl.RedirectionService.Bussines.Services;
using TinyUrl.RedirectionService.Data.Repositories;
using TinyUrl.RedirectionService.Infrastructure.Context;
using TinyUrl.RedirectionService.Infrastructure.Contracts.Options;
using TinyUrl.RedirectionService.Infrastructure.Repositories;
using TinyUrl.RedirectionService.Infrastructure.Services;

namespace TinyUrl.RedirectionService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();

            builder.Services.Configure<MongoDbOptions>(
                builder.Configuration.GetSection("MongoDbOptions"));

            builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value;
                return new MongoClient(options.ConnectionString);
            });

            builder.Services.AddScoped<MongoDbContext>();

            builder.Services.AddScoped<IUrlMappingService, UrlMappingService>();
            builder.Services.AddScoped<ICacheService, CacheService>();
            builder.Services.AddScoped<IUrlMappingRepository, UrlMappingRepository>();

            builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
