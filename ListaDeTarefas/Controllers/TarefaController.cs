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
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("", tarefa);
        }

        [HttpPut("atualizar/{id}")]
        public IActionResult AtualizarTarefa(int id, Tarefa tarefa)
        {
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
            var tarefa = _context.ListaDeTarefas.Find(id);

            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            _context.ListaDeTarefas.Remove(tarefa);
            _context.SaveChanges();

            return Ok("Tarefa Deletada!");
        }
    }
}
