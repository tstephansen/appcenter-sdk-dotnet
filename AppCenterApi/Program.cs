using AppCenterApi.Data;
using AppCenterApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewMind.Training.Api.Models.AppCenter;

namespace AppCenterApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", corsBuilder =>
            {
                corsBuilder.AllowAnyOrigin();
                corsBuilder.AllowAnyHeader();
                corsBuilder.AllowAnyMethod();
                corsBuilder.SetPreflightMaxAge(TimeSpan.FromMinutes(60));
            });
        });

        builder.Services.AddDbContext<ErrorsContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), opt =>
            {
                opt.EnableRetryOnFailure(maxRetryCount: 4, maxRetryDelay: TimeSpan.FromSeconds(1),
                    errorNumbersToAdd: new int[] { });
            });
        });

        builder.Services.AddScoped<IAppCenterService, AppCenterService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        var db = app.Services.GetRequiredService<ErrorsContext>();
        db.Database.EnsureCreated();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.MapControllers();
        app.MapPost("/Log", async ([FromServices] IAppCenterService appCenterService, LogRequest request) =>
            {
                var result = await appCenterService.LogAsync(request);
                return result;
            })
            .WithName("LogEvent")
            .WithOpenApi();
        app.MapGet("/", async context =>
        {
            await context.Response.WriteAsync("API is running.");
        });
        app.Run();
    }
}