using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoJWT.Data;
using TodoJWT.Models;
using TodoJWT_src.Interfaces;

namespace TodoJWT_src.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        public async Task<List<Usuario>> FindAllAsync(
            Contexto contexto)
        {
            // List<Usuario> usuarios = await contexto.Usuarios.AsNoTracking().ToListAsync();
            List<Usuario> usuarios = await contexto.Usuarios.AsNoTracking().ToListAsync();
            return usuarios;
        }

        public async Task<Usuario> FindById(Contexto contexto, int id)
        {
            Usuario usuario = await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return usuario;
        }

        public async Task<Usuario> CreateUserAsync(Contexto contexto, Usuario model)
        {
            try
            {
                await contexto.Usuarios.AddAsync(model);
                await contexto.SaveChangesAsync();
                return model;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Usuario> UpdateUserAsync(Contexto contexto, Usuario model, int id)
        {
            Usuario usuario = FindById(contexto, id).Result;
            try
            {
                if (model.Nome != null) usuario.Nome = model.Nome;
                if (model.Username != null) usuario.Username = model.Username;
                if (model.Password != null) usuario.Password = model.Password;

                contexto.Usuarios.Update(model);
                await contexto.SaveChangesAsync();

                return usuario;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteUserAsync(Contexto contexto, int id)
        {
            Usuario usuario = FindById(contexto, id).Result;

            if (usuario == null) return false;

            try
            {
                contexto.Usuarios.Remove(usuario);
                await contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
