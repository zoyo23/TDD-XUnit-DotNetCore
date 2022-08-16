using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class ConclusaoDaMatricula
    {
        #region Atributos
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        #endregion

        #region Construtores
        public ConclusaoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }
        #endregion

        #region Mètodos

        #region Métodos Públicos
        public void Concluir(int matriculaId, double notaDoAluno)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
                .Quando((matricula == null), Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            matricula.InformarNota(notaDoAluno);
        }
        #endregion

        #endregion
    }
}
