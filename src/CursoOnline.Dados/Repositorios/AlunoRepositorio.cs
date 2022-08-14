using CursoOnline.Dados.Contextos;
using CursoOnline.Dominio.Alunos;
using System.Linq;

namespace CursoOnline.Dados.Repositorios
{
    public class AlunoRepositorio : RepositorioBase<Aluno>, IAlunoRepositorio
    {

        #region Atributos

        #endregion

        #region Construtores
        public AlunoRepositorio(ApplicationDbContext context) : base(context) { }
        #endregion

        #region Métodos

        #region Métodos Públicos
        public Aluno ObterPeloCpf(string cpf)
        {
            var alunos = Context.Set<Aluno>().Where(a => a.Cpf == cpf);
            return alunos.Any() ? alunos.First() : null;
        }
        #endregion

        #endregion
    }
}
