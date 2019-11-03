using Bogus;
using CursoOnline.Dominio.Domain;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    //  Mock Vs Stub
    //  Mock Envolve a verificação do Dado
    //  Stub Serve para geração de Dado para validar regra de negócio

    public class ArmazenadorDeCursoTest
    {
        #region Atributos
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositoryMock;
        #endregion

        #region Construtores
        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };

            _cursoRepositoryMock = new Mock<ICursoRepositorio>();

            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositoryMock.Object);
        }
        #endregion

        #region Testes
        [Fact]
        public void DeveAdicionarCurso()
        {
            #region Arrange
            #endregion

            #region Act
            _armazenadorDeCurso.Armazenar(_cursoDto);
            #endregion

            #region Assert
            _cursoRepositoryMock.Verify(r => r.Adicionar(
                It.Is<Curso>(c =>
                    c.Nome.Equals(_cursoDto.Nome) &&
                    c.Descricao.Equals(_cursoDto.Descricao)
                ))  // Verifica se a instância recebida é a mesma esperada e se os atributos são idênticos aos enviados
            );
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositoryMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
            .ComMensagem("Nome do Curso já consta no banco de dados.");
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";

            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Algo inválido.");
        }
        #endregion
    }
}
