using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Models;
using System.Diagnostics;
using ListaDeTarefas.Data;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
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
            var logado = Request.Cookies["IdLogado"];
            if (logado == null)
                return Unauthorized("Faça login antes de atualizar seus dados");

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
        [HttpPost("login")]
        public IActionResult LoginUsuario(Usuario usuario)
        {
            var resultadoUsuario = _context.Usuarios.Where
                (u => u.Email.Equals(usuario.Email) &&
                u.Senha.Equals(usuario.Senha)).ToList();
            if (resultadoUsuario.Count == 0)
            {
                return Unauthorized("Email ou senha inválidos");
            }
            HttpContext.Session.SetString("IdLogado", resultadoUsuario[0].Id.ToString());
            Response.Cookies.Append("IdLogado", resultadoUsuario[0].Id.ToString(),
               new CookieOptions
               {
                   HttpOnly = true,
                   Secure = true,
                   SameSite = SameSiteMode.None
               });
            return Ok(new { mensagem = resultadoUsuario[0].Nome, sucesso = true });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("IdLogado");
            return Ok("Logout realizado");
        }



        [HttpGet("{id}")]
        public IActionResult SocilitaUsuarioID(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound("Pessoa não encontrada");
            }
            return Ok(usuario);
        }
    }
}
