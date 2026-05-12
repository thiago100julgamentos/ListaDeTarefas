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

        [HttpGet("mostrarTarefa{idUsuario}")]
        public IActionResult MostrarTarefaUsuario(int idUsuario)
        {
            var logado = Request.Cookies["IdLogado"];
            if (logado == null)
                return Unauthorized("Faça login antes de mostrar tarefas");

            var tarefas = _context.ListaDeTarefas.Where(t => t.IdUsuario == idUsuario).ToList();
            if (tarefas.Count == 0)
            {
                return NotFound("Usuário não encontrado");
            }
            return Ok(tarefas);
        }

        [HttpDelete("{id}")]
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
