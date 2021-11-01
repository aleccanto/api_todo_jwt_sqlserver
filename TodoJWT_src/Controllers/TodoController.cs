using System.Security.Claims;
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
    public class TodoController : ControllerBase
    {
        private readonly ITodoServices _todoServices = new TodoServices();

        [HttpGet]
        [Route("todos")]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(
            [FromServices] Contexto contexto)
        {
            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var todos = await _todoServices.FindAllAsync(contexto, idUser);
            return Ok(todos);
        }

        [HttpGet]
        [Route("todos/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id)
        {
            Todo todo = await _todoServices.FindById(contexto, id);
            if (todo == null) NotFound();

            return Ok(todo);
        }

        [HttpPost]
        [Route("todos")]
        [Authorize]
        public async Task<IActionResult> PostUserAsync(
            [FromServices] Contexto contexto,
            [FromBody] Todo model)
        {
            if (!ModelState.IsValid) return BadRequest();

            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Todo todo = await _todoServices.CreateTodoAsync(contexto, model, idUser);

            if (todo == null) return BadRequest();

            return Created($"v1/todos/{model.Id}", model);

            // try
            // {
            //     int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //     model.UsuarioId = UserId;
            //     await contexto.Todos.AddAsync(model);
            //     await contexto.SaveChangesAsync();
            //     return Created($"v1/todos/{model.Id}", model);
            // }
            // catch
            // {
            //     return BadRequest();
            // }
        }

        [HttpPut]
        [Route("todos/{id:int}")]
        [Authorize]
        public async Task<IActionResult> PutUserAsync(
            [FromServices] Contexto contexto,
            [FromBody] Todo model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest();

            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Todo todo = await _todoServices.UpdateTodoAsync(contexto, model, id, idUser);

            if (todo == null) return BadRequest();

            return Accepted(todo);

            // if (model.Title != null) todo.Title = model.Title;
            // if (model.Done) todo.Done = model.Done;

            // try
            // {
            //     contexto.Todos.Update(todo);
            //     await contexto.SaveChangesAsync();
            //     return Accepted(todo);
            // }
            // catch
            // {
            //     return BadRequest();
            // }
        }

        [HttpDelete]
        [Route("todos/{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] Contexto contexto,
            [FromRoute] int id)
        {
            // try
            // {
            //     int UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            //     Todo todo = await contexto.Todos.AsNoTracking().Where(x => x.UsuarioId == UserId).FirstOrDefaultAsync(x => x.Id == id);
            //     contexto.Todos.Remove(todo);
            //     await contexto.SaveChangesAsync();

            //     return Ok();
            // }
            // catch
            // {
            //     return NotFound();
            // }

            string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            bool resposta = await _todoServices.DeleteTodoAsync(contexto, id, idUser);
            if (!resposta) return NotFound();
            return Ok();
        }
    }
}
