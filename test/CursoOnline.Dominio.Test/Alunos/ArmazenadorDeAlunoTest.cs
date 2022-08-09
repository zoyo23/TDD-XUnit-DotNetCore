using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Domain;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Alunos
{
    public class ArmazenadorDeAlunoTest
    {
        #region Atributos
        private readonly Faker _faker;
        private AlunoDto _alunoDto;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        #endregion

        #region Construtores
        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();

            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = PublicoAlvo.Empregado.ToString()
            };

            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorio.Object);
        }
        #endregion

        #region Testes
        [Fact]
        public void DeveAdicionarAluno()
        {
            #region Arrange
            #endregion

            #region Act
            _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            _alunoRepositorio.Verify(r => r.Adicionar(It.Is<Aluno>(a => a.Nome == _alunoDto.Nome)));
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaFoiCadastrado()
        {
            #region Arrange
            var alunoComMesmoCpf = AlunoBuilder.Novo().ComId(34).Build();

            _alunoRepositorio.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf))
                .Returns(alunoComMesmoCpf);
            #endregion

            #region Act
            Action act = () => _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.CpfJaCadastrado);
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarAlunoQuandoPublicoAlvoInvalido()
        {
            #region Arrange
            const string publicoAlvoInvalido = "Inválido";
            _alunoDto.PublicoAlvo = publicoAlvoInvalido;
            #endregion

            #region Act
            Action act = () => _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.PublicoAlvoInvalido);
            #endregion
        }

        [Fact]
        public void DeveEditarNomeDoAluno()
        {
            #region Arrange
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id))
                .Returns(alunoJaSalvo);
            #endregion

            #region Act
            _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
            #endregion
        }

        [Fact]
        public void NaoDeveAlunoQuandoForEdicao()
        {
            #region Arrange
            _alunoDto.Id = 35;

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id))
                .Returns(alunoJaSalvo);
            #endregion

            #region Act
            _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            _alunoRepositorio.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
            #endregion
        }

        [Fact]
        public void NaoDeveEditarInformacoesAlemDoNomeDoAluno()
        {
            #region Arrange
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;

            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id))
                .Returns(alunoJaSalvo);
            #endregion

            #region Act
            _armazenadorDeAluno.Armazenar(_alunoDto);
            #endregion

            #region Assert
            Assert.Equal(cpfEsperado, alunoJaSalvo.Cpf);
            #endregion
        }
        #endregion
    }
}
