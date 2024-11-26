using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Serilog;
using Taskly.Tasks.Host.Endpoints;
using Taskly.Tasks.Repository.Registration;
using Taskly.Tasks.Services.Registration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddTaskServices();
builder.Services.AddRepository(builder.Configuration);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapTasksEndpoints();

app.Run();

