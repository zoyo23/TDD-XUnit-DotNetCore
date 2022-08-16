using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class CriacaoDaMatricula
    {
        #region Atributos
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly IMatriculaRepositorio _matriculaRepositorio;
        #endregion

        #region Construtores
        public CriacaoDaMatricula(IAlunoRepositorio alunoRepositorio,
                                  ICursoRepositorio cursoRepositorio,
                                  IMatriculaRepositorio matriculaRepositorio)
        {
            _alunoRepositorio = alunoRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _matriculaRepositorio = matriculaRepositorio;
        }
        #endregion

        #region Métodos

        #region Métodos Públicos
        public void Criar(MatriculaDto matriculaDto)
        {
            var aluno = _alunoRepositorio.ObterPorId(matriculaDto.AlunoId);
            var curso = _cursoRepositorio.ObterPorId(matriculaDto.CursoId);

            ValidadorDeRegra.Novo()
                .Quando(curso == null, Resource.CursoNaoEncontrado)
                .Quando(aluno == null, Resource.AlunoNaoEncontrado)
                .DispararExcecaoSeExistir();

            var matricula = new Matricula(aluno, curso, matriculaDto.ValorPago);

            _matriculaRepositorio.Adicionar(matricula);
        }
        #endregion

        #endregion
    }
}
