using MauiTodo;
using Microsoft.EntityFrameworkCore;
using MauiTodo.API.Models;

namespace MauiTodo.API.TodoDbContext;



public class TodoContext: DbContext
{
    DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string conn_string = @"Server=labVMH8OX\SQLEXPRESS;Database=MauiTodo;TrustServerCertificate=True;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(conn_string);
        //Scaffold-DbContext "Server=localhost\SQLEXPRESS;Database=MauiTodo;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Model -t table-name
    }
}
