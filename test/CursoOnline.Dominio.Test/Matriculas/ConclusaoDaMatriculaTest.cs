using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
    public class ConclusaoDaMatriculaTest
    {
        #region Atributos
        private readonly Mock<IMatriculaRepositorio> _mockMatriculaRepositorio;
        private ConclusaoDaMatricula _conclusaoDaMatricula;
        #endregion

        #region Construtores
        public ConclusaoDaMatriculaTest()
        {
            _mockMatriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _conclusaoDaMatricula = new ConclusaoDaMatricula(_mockMatriculaRepositorio.Object);
        }
        #endregion

        #region Testes
        [Fact]
        public void DeveInformarNotaDoAluno()
        {
            #region Arrange
            var notaDoAlunoEsperada = 8;
            var matricula = MatriculaBuilder.Novo().Build();
            var conclusaoDaMatricula = new ConclusaoDaMatricula(_mockMatriculaRepositorio.Object);

            _mockMatriculaRepositorio
                .Setup(r => r.ObterPorId(matricula.Id))
                .Returns(matricula);
            #endregion

            #region Act
            conclusaoDaMatricula.Concluir(matricula.Id, notaDoAlunoEsperada);
            #endregion

            #region Assert
            Assert.Equal(notaDoAlunoEsperada, matricula.NotaDoAluno);
            #endregion
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            #region Arrange
            Matricula matriculaInvalida = null;
            var matriculaIdInvalida = 1;
            var notaDoAluno = 2;

            _mockMatriculaRepositorio
                .Setup(r => r.ObterPorId(matriculaIdInvalida))
                .Returns(matriculaInvalida);
            #endregion

            #region Act
            Action act = () => _conclusaoDaMatricula.Concluir(matriculaIdInvalida, notaDoAluno);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.MatriculaNaoEncontrada);
            #endregion
        }
        #endregion
    }
}
