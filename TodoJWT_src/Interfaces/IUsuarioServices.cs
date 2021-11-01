using System.Collections.Generic;
using System.Threading.Tasks;
using TodoJWT.Data;
using TodoJWT.Models;

namespace TodoJWT_src.Interfaces
{
    public interface IUsuarioServices
    {
        Task<List<Usuario>> FindAllAsync(Contexto contexto);

        Task<Usuario> FindById(Contexto contexto, int id);

        Task<Usuario> CreateUserAsync(Contexto contexto, Usuario model);

        Task<Usuario> UpdateUserAsync(Contexto contexto, Usuario model, int id);

        Task<bool> DeleteUserAsync(Contexto contexto, int id);
    }
}