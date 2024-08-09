
using MauiTodo.API.Models;
using MauiTodo.API.TodoDbContext;
using Microsoft.EntityFrameworkCore;
namespace MauiTodo.API.Services{
    public interface ITodos {


        Task<Todo[]> GetAllTodos();
        
    }


    public class TodosImpl : ITodos {

        public TodosImpl() { }

        public async  Task<Todo[]> GetAllTodos() {

            using (var context = new TodoContext()) {
                var selected = await context.Set<Todo>().ToArrayAsync();
                
                return selected;

            }

        }
    }


        
}
