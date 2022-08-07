using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Domain;
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
        private readonly Faker _faker;
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
            _faker = new Faker();

            _nome = _faker.Random.Word();
            _cargaHoraria = _faker.Random.Double(50, 1000);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = _faker.Random.Double(100, 1000);
            _descricao = _faker.Lorem.Paragraph();
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
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo()
                .ComNome(nomeInvalido);
            #endregion

            #region Act (Ação)
            Action act = () => curso.Build();
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Nome Inválido.");
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo()
                .ComCargaHoraria(cargaHorariaInvalida);
            #endregion

            #region Act (Ação)
            Action act = () => curso.Build();
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Carga horária deve ser maior que 1 hora.");
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorInvalido(double ValorInvalido)
        {
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo()
                    .ComValor(ValorInvalido);
            #endregion

            #region Act (Ação)
            Action act = () => curso.Build();
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Valor deve ser maior que R$1,00.");
            #endregion
        }

        [Fact]
        public void DeveAlterarNome()
        {
            #region Arrange
            var nomeEsperado = _faker.Person.FullName;
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act
            curso.AlterarNome(nomeEsperado);
            #endregion

            #region Assert
            Assert.Equal(nomeEsperado, curso.Nome);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveAlterarComNomeInvalido(string nomeInvalido)
        {
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act (Ação)
            Action act = () => curso.AlterarNome(nomeInvalido);
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Nome Inválido.");
            #endregion
        }

        [Fact]
        public void DeveAlterarCargaHoraria()
        {
            #region Arrange
            var cargaHorariaEsperada = 20.5;
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act
            curso.AlterarCargaHoraria(cargaHorariaEsperada);
            #endregion

            #region Assert
            Assert.Equal(cargaHorariaEsperada, curso.CargaHoraria);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarCargaHorariaInvalida(double cargaHorariaInvalida)
        {
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act (Ação)
            Action act = () => curso.AlterarCargaHoraria(cargaHorariaInvalida);
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Carga horária deve ser maior que 1 hora.");
            #endregion
        }

        [Fact]
        public void DeveAlterarValor()
        {
            #region Arrange
            var valorEsperado = 234.99;
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act
            curso.AlterarValor(valorEsperado);
            #endregion

            #region Assert
            Assert.Equal(valorEsperado, curso.Valor);
            #endregion
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveAlterarvalorInvalido(double valorInvalido)
        {
            #region Arrange (Organização)
            var curso = CursoBuilder.Novo().Build();
            #endregion

            #region Act (Ação)
            Action act = () => curso.AlterarValor(valorInvalido);
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem("Valor deve ser maior que R$1,00.");
            #endregion
        }
        #endregion
    }
}
