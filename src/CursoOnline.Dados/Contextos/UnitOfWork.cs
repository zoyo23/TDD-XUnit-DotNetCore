using CursoOnline.Dominio._Base;
using System.Threading.Tasks;

namespace CursoOnline.Dados.Contextos
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Atributos
        private readonly ApplicationDbContext _context;
        #endregion

        #region Construtores
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Métodos
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
