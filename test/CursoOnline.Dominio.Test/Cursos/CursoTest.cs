using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class CursoTest : IDisposable
    {
        #region Atributos
        private readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        #endregion

        #region Construtores e Dispose
        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo Executado.");

            _nome = "Informática Básica";
            _cargaHoraria = 80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = 950;
            _descricao = "Uma descrição";
        }

        public void Dispose()
        {
            _output.WriteLine("Dispose sendo Executado.");
        }
        #endregion

        #region Testes
        [Fact]
        public void DeveCriarCurso()
        {
            #region Arrange (Organização)
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };
            #endregion

            #region Act (Ação)
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);
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
            #endregion

            #region Act (Ação)
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComNome(nomeInvalido).Build()
            ).ComMensagem("Nome obrigatório.");
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            #region Arrange (Organização)
            #endregion

            #region Act (Ação)
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build()
            ).ComMensagem("Carga horária deve ser maior que 1 hora.");
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQue1(double ValorInvalido)
        {
            #region Arrange (Organização)
            #endregion

            #region Act (Ação)
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(ValorInvalido).Build()
            ).ComMensagem("Valor deve ser maior que R$1,00.");
            #endregion
        }
        #endregion
    }

    public class Curso
    {
        #region Constructors
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
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
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
        #endregion

        #region Attributes
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        #endregion
    }

    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }
}
