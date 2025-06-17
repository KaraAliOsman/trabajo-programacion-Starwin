using GestionProyectosStarwin.Modelos;
using Microsoft.EntityFrameworkCore;

namespace GestionProyectosStarwin.Datos
{
    public class AppContexto : DbContext
    {
        public DbSet<Proyecto> Proyectos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=starwin_proyectos.db");
        }
    }
}