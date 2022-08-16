using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Matriculas
{
    public class CancelamentoDaMatricula
    {
        #region Atributos
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        #endregion

        #region Construtores
        public CancelamentoDaMatricula(IMatriculaRepositorio matriculaRepositorio)
        {
            _matriculaRepositorio = matriculaRepositorio;
        }
        #endregion

        #region Métodos

        #region Mètodos Públicos
        public void Cancelar(int matriculaId)
        {
            var matricula = _matriculaRepositorio.ObterPorId(matriculaId);

            ValidadorDeRegra.Novo()
                .Quando((matricula == null), Resource.MatriculaNaoEncontrada)
                .DispararExcecaoSeExistir();

            matricula.Cancelar();
        }
        #endregion

        #endregion
    }
}
