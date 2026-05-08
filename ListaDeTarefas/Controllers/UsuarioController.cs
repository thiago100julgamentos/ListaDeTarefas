using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Models;
using System.Diagnostics;
using ListaDeTarefas.Data;
using System.Reflection.Metadata.Ecma335;
namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ListaDeTarefasContext _context;

        public UsuarioController(ListaDeTarefasContext context)
        {
            _context = context;
        }
    [HttpPost("cadastrar")]
    public IActionResult CadastraUsuario(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
            return Created("", usuario);
        }
    [HttpPut("atualizar/{id}")]
    public IActionResult AtualizarUsuario(int id, Usuario usuario)
        {
            var usuarioBanco = _context.Usuarios.Find(id);
            if (usuarioBanco == null)
            {
                return NotFound("Pessoa não encontrada.");
            }
            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Senha = usuario.Senha;
            _context.SaveChanges();
            return Ok("Atualizado!");
        }
    [HttpDelete("{id}")]
    public IActionResult DeletarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
                return NotFound("Pessoa não encontrada.");

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return Ok("Usuario deletado!");
        }


    }
}
