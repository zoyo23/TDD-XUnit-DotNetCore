using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        #region Atributos
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositoryMock;
        private readonly Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;

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
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositoryMock.Object, _conversorDePublicoAlvo.Object);
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
                    )
                )
            );
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            #region Arrange (Organização)
            var cursoJaSalvo = CursoBuilder.Novo().ComId(432).ComNome(_cursoDto.Nome).Build();
            _cursoRepositoryMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
            #endregion

            #region Act (Ação)
            Action act = () => _armazenadorDeCurso.Armazenar(_cursoDto);
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.NomeCursoJaExiste);
            #endregion
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            #region Arrange
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(r => r.ObterPorId(_cursoDto.Id))
                .Returns(curso);
            #endregion

            #region Act
            _armazenadorDeCurso.Armazenar(_cursoDto);
            #endregion

            #region Assert
            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            #region Arrange
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();

            _cursoRepositoryMock.Setup(r => r.ObterPorId(_cursoDto.Id))
                .Returns(curso);
            #endregion

            #region Act
            _armazenadorDeCurso.Armazenar(_cursoDto);
            #endregion

            #region Assert
            _cursoRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
            #endregion
        }
        #endregion
    }
}
