using System.Collections.Generic;
using System.Threading.Tasks;
using TodoJWT.Data;
using TodoJWT.Models;

namespace TodoJWT_src.Interfaces
{
    public interface ITodoServices
    {
        Task<List<Todo>> FindAllAsync(Contexto contexto, string idUser);

        Task<Todo> FindById(Contexto contexto, int id);

        Task<Todo> CreateTodoAsync(Contexto contexto, Todo model, string idUser);

        Task<Todo> UpdateTodoAsync(Contexto contexto, Todo model, int id, string idUser);

        Task<bool> DeleteTodoAsync(Contexto contexto, int id, string idUser);
    }
}