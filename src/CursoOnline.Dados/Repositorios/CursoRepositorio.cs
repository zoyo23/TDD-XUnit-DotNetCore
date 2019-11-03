using CursoOnline.Dados.Contextos;
using CursoOnline.Dados.Repositorios;
using CursoOnline.Dominio.Domain;
using System.Linq;

namespace CursoOnline.Dados
{
    public class CursoRepositorio : RepositorioBase<Curso>, ICursoRepositorio
    {
        #region Construtores
        public CursoRepositorio(ApplicationDbContext context)
            : base(context) { }
        #endregion

        #region Métodos
        public Curso ObterPeloNome(string nome)
        {
            var entidade = Context.Set<Curso>().Where(c => c.Nome.Contains(nome));
            if (entidade.Any())
                return entidade.First();
            return null;
        }
        #endregion
    }
}
