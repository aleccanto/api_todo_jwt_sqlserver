using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoJWT.Data;
using TodoJWT.Models;
using TodoJWT_src.Interfaces;

namespace TodoJWT_src.Services
{
    public class TodoServices : ITodoServices
    {
        public async Task<Todo> CreateTodoAsync(Contexto contexto, Todo model, string idUser)
        {
            try
            {
                int UserId = int.Parse(idUser);
                model.UsuarioId = UserId;
                await contexto.Todos.AddAsync(model);
                await contexto.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteTodoAsync(Contexto contexto, int id, string idUser)
        {

            try
            {
                int UserId = int.Parse(idUser);
                Todo todo = await contexto.Todos.AsNoTracking().Where(x => x.UsuarioId == UserId).FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null) return false;
                contexto.Todos.Remove(todo);
                await contexto.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Todo>> FindAllAsync(Contexto contexto, string idUser)
        {
            int UserId = int.Parse(idUser);
            List<Todo> todos = await contexto.Todos.AsNoTracking().Where(x => x.UsuarioId == UserId).ToListAsync();
            return todos;
        }

        public async Task<Todo> FindById(Contexto contexto, int id)
        {
            Todo todo = await contexto.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (todo == null) return todo;

            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(Contexto contexto, Todo model, int id, string idUser)
        {
            int UserId = int.Parse(idUser);
            Todo todo = await contexto.Todos.AsNoTracking().Where(x => x.UsuarioId == UserId).FirstOrDefaultAsync(x => x.Id == id);

            if (model.Title != null) todo.Title = model.Title;
            if (model.Done) todo.Done = model.Done;

            try
            {
                contexto.Todos.Update(todo);
                await contexto.SaveChangesAsync();
                return todo;
            }
            catch
            {
                return null;
            }
        }
    }
}