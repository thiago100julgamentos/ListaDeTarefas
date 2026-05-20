using Microsoft.AspNetCore.Mvc;
using ListaDeTarefas.Models;
using System.Diagnostics;
using ListaDeTarefas.Data;
using System.Reflection.Metadata.Ecma335;
namespace ListaDeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ListaDeTarefasContext _context;

        public TarefaController(ListaDeTarefasContext context)
        {
            _context = context;
        }

        [HttpPost("criarTarefa")]
        public IActionResult CriarTarefa(Tarefa tarefa)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado == null)
                return Unauthorized("Faça login antes de criar tarefas");

            var id = Request.Cookies["IdLogado"];
            if ( id != null)
            {
                tarefa.IdUsuario = int.Parse(id);
            }

            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado == null)
                return Unauthorized("Faça login antes de atualizar tarefas");

            var tarefaBanco = _context.ListaDeTarefas.Find(id);
            if (tarefaBanco == null)
            {
                return NotFound("Tarefa não encontrada.");
            }
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Status = tarefa.Status;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpGet("mostrarTarefa")]
        public IActionResult MostrarTarefaUsuario(int idUsuario)
        {
            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");
            if (sessaoUsuario == null)
            {
                return Unauthorized("Faça login antes");
            }
            var idLogado = Request.Cookies["IdLogado"];
            if (idLogado != null)
            {
                var resultado = from u in _context.Usuarios
                                join t in _context.ListaDeTarefas
                                on u.Id equals t.IdUsuario
                                where u.Id == int.Parse(idLogado)
                                select new
                                {
                                    Usuario = u.Nome,
                                    u.Email,
                                    Tarefas = t.Descricao,
                                    t.Status
                                };
                return Ok(resultado.ToList());
            }
            return Unauthorized("Faça login antes");
        }

        [HttpDelete("deletar")]
        public IActionResult DeletarTarefa(int id)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado == null)
                return Unauthorized("Faça login antes de deletar tarefas");

            var tarefa = _context.ListaDeTarefas.Find(id);

            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            _context.ListaDeTarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Tarefa Deletada!");
        }
    }
}
