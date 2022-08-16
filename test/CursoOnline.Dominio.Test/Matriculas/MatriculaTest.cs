using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Matriculas
{
    public class MatriculaTest
    {
        #region Atributos

        #endregion

        #region Construtores

        #endregion

        #region Testes
        [Fact]
        public void DeveCriarMatricula()
        {
            #region Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empreendedor).Build();
            var matriculaEsperada = new
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };
            #endregion

            #region Act
            var matricula = new Matricula(matriculaEsperada.Aluno, matriculaEsperada.Curso, matriculaEsperada.ValorPago);
            #endregion

            #region Assert
            matriculaEsperada.ToExpectedObject().ShouldMatch(matricula);
            #endregion
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemAluno()
        {
            #region Arrange
            Aluno alunoInvalido = null;
            #endregion

            #region Act
            Action act = () => MatriculaBuilder.Novo().ComAluno(alunoInvalido).Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.AlunoInvalido);
            #endregion
        }

        [Fact]
        public void NaoDeveCriarMatriculaSemCurso()
        {
            #region Arrange
            Curso cursoInvalido = null;
            #endregion

            #region Act
            Action act = () => MatriculaBuilder.Novo().ComCurso(cursoInvalido).Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.CursoInvalido);
            #endregion
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoInvalido()
        {
            #region Arrange
            const double valorPagoInvalido = 0;
            #endregion

            #region Act
            Action act = () => MatriculaBuilder.Novo().ComValorPago(valorPagoInvalido).Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.ValorPagoInvalido);
            #endregion
        }

        [Fact]
        public void NaoDeveCriarMatriculaComValorPagoMaiorQueValorDoCurso()
        {
            #region Arrange
            var curso = CursoBuilder.Novo().ComValor(100).Build();
            double ValorPagoMaiorQueCurso = curso.Valor + 10;
            #endregion

            #region Act
            Action act = () => MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(ValorPagoMaiorQueCurso).Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.ValorPagoMaiorQueValorDoCurso);
            #endregion
        }

        [Fact]
        public void DeveIndicarQueHouveDescontoNaMatricula()
        {
            #region Arrange
            var curso = CursoBuilder.Novo().ComValor(100).ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empreendedor).Build();
            double ValorPagoComDesconto = curso.Valor - 10;
            #endregion

            #region Act
            var matricula = MatriculaBuilder.Novo().ComCurso(curso).ComValorPago(ValorPagoComDesconto).Build();
            #endregion

            #region Assert
            Assert.True(matricula.TemDesconto);
            #endregion
        }

        [Fact]
        public void NaoDevePublicoAlvoDeAlunoECursoSeremDiferentes()
        {
            #region Arrange
            var curso = CursoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empregado).Build();
            var aluno = AlunoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Universitario).Build();
            #endregion

            #region Act
            Action act = () => MatriculaBuilder.Novo().ComCurso(curso).ComAluno(aluno).Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.PublicosAlvoDiferentes);
            #endregion
        }
        #endregion
    }
}
