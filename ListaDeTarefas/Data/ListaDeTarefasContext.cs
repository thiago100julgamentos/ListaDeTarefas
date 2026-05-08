using Microsoft.EntityFrameworkCore;
using ListaDeTarefas.Models;
namespace ListaDeTarefas.Data
{
    public class ListaDeTarefasContext : DbContext
    {
        public ListaDeTarefasContext(DbContextOptions<ListaDeTarefasContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> ListaDeTarefas { get; set; }
    }
}
