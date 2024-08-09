
using MauiTodo.API.DTOs;
using MauiTodo.API.Models;
using MauiTodo.API.TodoDbContext;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
//using MauiTodo.API.Models;

namespace MauiTodo.API;

public class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);
        var URL = "https://vbmlbsjepwswcjfbllwc.supabase.co";
        var KEY = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZibWxic2plcHdzd2NqZmJsbHdjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjE3MzMwMjAsImV4cCI6MjAzNzMwOTAyMH0.8yigSd1yaDjJaSYt5k4hUEe9SA6XswpLigeAcQ8BHVk";

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<Supabase.Client>(_ => new Supabase.Client(URL, KEY));
        //name server -> labVMH8OX\SQLEXPRESS
        // Project Url -> https://vbmlbsjepwswcjfbllwc.supabase.co
        /*Project Key -> eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InZibWxic2plcHdzd2NqZmJsbHdjIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MjE3MzMwMjAsImV4cCI6MjAzNzMwOTAyMH0.8yigSd1yaDjJaSYt5k4hUEe9SA6XswpLigeAcQ8BHVk
         */

        var _context = builder.Services.AddScoped<TodoContext>(_ => new TodoContext());

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", (HttpContext httpContext) => {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        //Newsletter min Api Endpoints
        app.MapPost("/newsletters", async (CreateNewsletterRequest request, Supabase.Client client) => {
            var newsletter = new Newsletter {
                Name = request.Name,
                Description = request.Description,
                ReadTime = request.ReadTime,
            };
            var response = await client.From<Newsletter>().Insert(newsletter);

            var newNewsLetter = response.Models.First();

            return Results.Ok(newNewsLetter.Id);



        });


        //Get Todos
        app.MapGet("/todos/{id}", async (int id) => {
            using var _context = new TodoContext();

            var response = await _context.FindAsync<Todo>(id);


            if (response is null) return Results.NotFound();

            var todo = new Todo {
                Id = response.Id,
                Title = response.Title

            };

            return Results.Ok(todo);

        });

        app.MapGet("/todos", async () => {

            using var _context = new TodoContext();

            var todos = await (from todo in _context.Set<Todo>()
                        select todo)
                        .ToListAsync();

            if(todos == null) return Results.NotFound();    


            var selected = await _context.Set<Todo>()
            .ToListAsync();

            return Results.Ok(todos);

        });



        app.MapDelete("/deleteTodo/{id}", async (long id) => {

            using var _context = new TodoContext();

            var response = await _context.FindAsync<Todo>(id);  

            if(response is null) {  
                return Results.NotFound();

            } 

           _context.Remove(response);
            await _context.SaveChangesAsync();

            return Results.NoContent();
        });

        //Add Todo
        app.MapPost("/todo", async (Todo todo) => {
            using var _context = new TodoContext();

           await  _context.AddAsync<Todo>(todo);
            await _context.SaveChangesAsync();

            return Results.Ok(todo);


        });

        //Update Todo
        app.MapPut("todo/{id}", async (long id, Todo todo) => {
            using var _context = new TodoContext();

            var todo_result = await _context.Set<Todo>()
            .FirstOrDefaultAsync(t => t.Id == id);

            if (todo_result is null) {
                return Results.NotFound();
            }

            todo_result.Title = todo.Title;

            await _context.SaveChangesAsync();

            return Results.NoContent();

        });


        app.Run();
    }
}
