using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
    public class CriacaoDaMatriculaTest
    {
        #region Atributos
        private readonly Mock<IAlunoRepositorio> _mockAlunoRepositorio;
        private readonly Mock<ICursoRepositorio> _mockCursoRepositorio;
        private readonly Mock<IMatriculaRepositorio> _mockMatriculaRepositorio;

        private readonly CriacaoDaMatricula _criacaoDaMatricula;
        private readonly MatriculaDto _matriculaDto;
        private readonly Aluno _aluno;
        private readonly Curso _curso;
        #endregion

        #region Construtores
        public CriacaoDaMatriculaTest()
        {
            _mockAlunoRepositorio = new Mock<IAlunoRepositorio>();
            _mockCursoRepositorio = new Mock<ICursoRepositorio>();
            _mockMatriculaRepositorio = new Mock<IMatriculaRepositorio>();

            _criacaoDaMatricula = new CriacaoDaMatricula(_mockAlunoRepositorio.Object, _mockCursoRepositorio.Object, _mockMatriculaRepositorio.Object);

            _aluno = AlunoBuilder.Novo().ComId(23).ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Universitario).Build();
            _mockAlunoRepositorio.Setup(r => r.ObterPorId(_aluno.Id)).Returns(_aluno);

            _curso = CursoBuilder.Novo().ComId(32).ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Universitario).Build();
            _mockCursoRepositorio.Setup(r => r.ObterPorId(_curso.Id)).Returns(_curso);

            _matriculaDto = new MatriculaDto { AlunoId = _aluno.Id, CursoId = _curso.Id, ValorPago = _curso.Valor };

        }
        #endregion

        #region Testes
        [Fact]
        public void DeveNotificarQuandoCursoNaoForEncontrado()
        {
            #region Arrange
            Curso cursoInvalido = null;

            _mockCursoRepositorio
                .Setup(r => r.ObterPorId(_matriculaDto.CursoId))
                .Returns(cursoInvalido);
            #endregion

            #region Act
            Action act = () => _criacaoDaMatricula.Criar(_matriculaDto);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.CursoNaoEncontrado);
            #endregion
        }

        [Fact]
        public void DeveNotificarQuandoAlunoNaoForEncontrado()
        {
            #region Arrange
            Aluno alunoInvalido = null;

            _mockAlunoRepositorio
                .Setup(r => r.ObterPorId(_matriculaDto.AlunoId))
                .Returns(alunoInvalido);
            #endregion

            #region Act
            Action act = () => _criacaoDaMatricula.Criar(_matriculaDto);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.AlunoNaoEncontrado);
            #endregion
        }

        [Fact]
        public void DeveAdicionarMatricula()
        {
            #region Arrange
            #endregion

            #region Act
            _criacaoDaMatricula.Criar(_matriculaDto);
            #endregion

            #region Assert
            _mockMatriculaRepositorio.Verify(r => r.Adicionar(It.Is<Matricula>(m => m.Aluno == _aluno && m.Curso == _curso)));
            #endregion
        }
        #endregion
    }
}
