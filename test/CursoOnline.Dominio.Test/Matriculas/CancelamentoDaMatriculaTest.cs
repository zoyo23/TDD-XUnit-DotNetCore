using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
    public class CancelamentoDaMatriculaTest
    {
        #region Atributos
        private readonly Mock<IMatriculaRepositorio> _mockMatriculaRepositorio;
        private readonly CancelamentoDaMatricula _cancelamentoDaMatricula;

        #endregion

        #region Construtores
        public CancelamentoDaMatriculaTest()
        {

            _mockMatriculaRepositorio = new Mock<IMatriculaRepositorio>();
            _cancelamentoDaMatricula = new CancelamentoDaMatricula(_mockMatriculaRepositorio.Object);
        }
        #endregion

        #region Testes
        [Fact]
        public void DeveCancelarMatricula()
        {
            #region Arrange
            var matricula = MatriculaBuilder.Novo().Build();
            _mockMatriculaRepositorio
                .Setup(r => r.ObterPorId(matricula.Id))
                .Returns(matricula);
            #endregion

            #region Act
            _cancelamentoDaMatricula.Cancelar(matricula.Id);
            #endregion

            #region Assert
            Assert.True(matricula.Cancelada);
            #endregion
        }

        [Fact]
        public void DeveNotificarQuandoMatriculaNaoEncontrada()
        {
            #region Arrange
            Matricula matriculaInvalida = null;
            var matriculaIdInvalida = 1;

            _mockMatriculaRepositorio
                .Setup(r => r.ObterPorId(matriculaIdInvalida))
                .Returns(matriculaInvalida);
            #endregion

            #region Act
            Action act = () =>_cancelamentoDaMatricula.Cancelar(matriculaIdInvalida);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.MatriculaNaoEncontrada);
            #endregion
        }
        #endregion
    }
}
