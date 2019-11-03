using CursoOnline.Dominio.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contextos
{
    public class ApplicationDbContext : DbContext
    {
        #region Construtores
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
        #endregion

        #region Atributos / DBSet
        public DbSet<Curso> Cursos { get; set; }
        #endregion

        #region Métodos 
        #region Métodos Publicos
        public async Task Commit()
        {
            await SaveChangesAsync();
        }
        #endregion

        #region Métodos Protegidos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
        #endregion
    }
}
