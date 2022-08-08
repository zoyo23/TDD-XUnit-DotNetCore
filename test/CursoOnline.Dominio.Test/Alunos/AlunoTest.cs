using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Domain;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using ExpectedObjects;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class AlunoTest
    {
        #region Atributos
        private readonly Faker _faker;

        private readonly string _nome;
        private readonly string _email;
        private readonly string _cpf;
        private readonly PublicoAlvo _publicoAlvo;
        #endregion

        #region Construtores
        public AlunoTest()
        {
            _faker = new Faker();
        }

        #endregion

        #region Testes
        [Fact]
        public void DeveCriarAluno()
        {
            #region Arrange
            var alunoEsperado = new
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Estudante
            };
            #endregion

            #region Act
            var aluno = new Aluno(alunoEsperado.Nome, alunoEsperado.Email, alunoEsperado.Cpf, alunoEsperado.PublicoAlvo);
            #endregion

            #region Assert
            alunoEsperado.ToExpectedObject().ShouldMatch(aluno);
            #endregion
        }

        [Fact]
        public void DeveAlterarNome()
        {
            #region Arrange
            var novoNomeEsperado = _faker.Person.FullName;
            var aluno = AlunoBuilder.Novo().Build();
            #endregion

            #region Act
            aluno.AlterarNome(novoNomeEsperado);
            #endregion

            #region Assert
            Assert.Equal(novoNomeEsperado, aluno.Nome);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCriarComNomeInvalido(string nomeInvalido)
        {
            #region Arrange
            var builder = AlunoBuilder.Novo().ComNome(nomeInvalido);
            #endregion

            #region Act
            Action act = () => builder.Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.NomeInvalido);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("email invalido")]
        [InlineData("email@invalido")]
        public void NaoDeveCriarComEmailInvalido(string emailInvalido)
        {
            #region Arrange
            var builder = AlunoBuilder.Novo().ComEmail(emailInvalido);
            #endregion

            #region Act
            Action act = () => builder.Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.EmailInvalido);
            #endregion
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("CPF Inlivalido")]
        [InlineData("0000000000")]
        public void NaoDeveCriarComCpfInvalido(string cpfInvalido)
        {
            #region Arrange
            var builder = AlunoBuilder.Novo().ComCpf(cpfInvalido);
            #endregion

            #region Act
            Action act = () => builder.Build();
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.CpfInvalido);
            #endregion
        }
        #endregion
    }
}
