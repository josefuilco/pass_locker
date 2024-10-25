using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PassLocker.Application;
using PassLocker.Infrastructure;
using PassLocker.Infrastructure.Connection;

var myAllowSpecificOrigin = "MyAllowSpecificOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
    options.AddPolicy(myAllowSpecificOrigin, policy =>
        policy.WithOrigins("http://localhost:5173")
            .WithHeaders(new string[] {
                "Content-Type: application/json"
            }).AllowAnyMethod()));

builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PassLockerDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigin);

app.UseAuthorization();

app.MapControllers();

app.Run();
