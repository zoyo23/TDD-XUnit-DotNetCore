using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            #region Arrange (Organização)
            var cursoEsperado = new
            {
                Nome = "Informática Básica",
                CargaHoraria = (double)80,
                PublicoAlvo = "Estudantes",
                Valor = (double)950
            };
            #endregion

            #region Act (Ação)
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
            #endregion

            #region Assert (Afirmação)
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            #region Arrange (Organização
            var cursoEsperado = new
            {
                Nome = "Informática Básica",
                CargaHoraria = (double)80,
                PublicoAlvo = "Estudantes",
                Valor = (double)950
            };
            #endregion

            #region Act (Ação)
            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
            ).Message;
            #endregion

            #region Assert (Afirmação)
            Assert.Equal("Nome obrigatório.", message);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            #region Arrange (Organização
            var cursoEsperado = new
            {
                Nome = "Informática Básica",
                CargaHoraria = (double)80,
                PublicoAlvo = "Estudantes",
                Valor = (double)950
            };
            #endregion

            #region Act (Ação)
            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor)
            ).Message;
            #endregion

            #region Assert (Afirmação)
            Assert.Equal("Carga horária deve ser maior que 1 hora.", message);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double ValorInvalido)
        {
            #region Arrange (Organização
            var cursoEsperado = new
            {
                Nome = "Informática Básica",
                CargaHoraria = (double)80,
                PublicoAlvo = "Estudantes",
                Valor = (double)950
            };
            #endregion

            #region Act (Ação)
            var message = Assert.Throws<ArgumentException>(() =>
                new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, ValorInvalido)
            ).Message;
            #endregion

            #region Assert (Afirmação)
            Assert.Equal("Valor deve ser maior que R$1,00.", message);
            #endregion
        }
    }

    public class Curso
    {
        #region Constructors
        public Curso(string nome, double cargaHoraria, string publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome obrigatório.");
            }

            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária deve ser maior que 1 hora.");
            }

            if (valor < 1)
            {
                throw new ArgumentException("Valor deve ser maior que R$1,00.");
            }

            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
        #endregion

        #region Attributes
        public string Nome { get; private set; }
        public double CargaHoraria { get; private set; }
        public string PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        #endregion
    }
}
