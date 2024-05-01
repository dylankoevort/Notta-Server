using Application;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service;
using Google.Cloud.Firestore;

var builder = WebApplication.CreateBuilder(args);
const string serviceName = "Notta";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = serviceName, Version = "v1" });
});

// CORS
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()));

// Logging
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
builder.Services.AddScoped<FirebaseContext>();
builder.Services.AddScoped<ILogger<FirebaseContext>, Logger<FirebaseContext>>();

builder.Services.AddSingleton<DatabaseTester>();

// Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INotebookRepository, NotebookRepository>();
builder.Services.AddScoped<INoteRepository, NoteRepository>();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INotebookService, NotebookService>();
builder.Services.AddScoped<INoteService, NoteService>();


const string pathBase = "/notta";

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notta API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAll");

var databaseTester = app.Services.GetRequiredService<DatabaseTester>();
databaseTester.TestConnection();

await app.RunAsync();
