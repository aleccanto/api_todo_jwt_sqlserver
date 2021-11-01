using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoJWT.Data;
using TodoJWT.Models;
using TodoJWT_src.Interfaces;
using TodoJWT_src.Services;

namespace TodoJWT.Controllers
{

    [ApiController]
    [Route("v1")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServices _usuarioService = new UsuarioServices();

        [HttpGet]
        [Route("usuarios")]
        // [AllowAnonymous]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(
            [FromServices] Contexto contexto)
        {
            List<Usuario> todos = await _usuarioService.FindAllAsync(contexto);
            return Ok(todos);
        }

        [HttpGet]
        [Route("usuarios/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id)
        {
            Usuario usuario = await _usuarioService.FindById(contexto, id);
            if (usuario == null) NotFound();

            return Ok(usuario);
        }

        [HttpPost]
        [Route("usuarios")]
        [AllowAnonymous]
        public async Task<IActionResult> PostUserAsync(
            [FromServices] Contexto contexto,
            [FromBody] Usuario model)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _usuarioService.CreateUserAsync(contexto, model);
                return Created($"v1/usuarios/{model.Id}", model);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("usuarios/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> PutUserAsync(
            [FromServices] Contexto contexto,
            [FromBody] Usuario model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            Usuario usuario = await _usuarioService.UpdateUserAsync(contexto, model, id);

            if (usuario == null) return BadRequest();

            return Accepted(usuario);
        }

        [HttpDelete]
        [Route("usuarios/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id)
        {
            bool result = await _usuarioService.DeleteUserAsync(contexto, id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
