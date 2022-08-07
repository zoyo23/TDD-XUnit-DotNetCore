using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Domain;
using CursoOnline.Dominio.Test._Builders;
using CursoOnline.Dominio.Test._Util;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
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
                    )
                )
            );
            #endregion
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            #region Arrange (Organização)
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositoryMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);
            #endregion

            #region Act (Ação)
            #endregion            

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(Resource.NomeCursoJaExiste);
            #endregion
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            #region Arrange (Organização)
            var publicoAlvoInvalido = "Médico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;
            #endregion

            #region Act (Ação)
            #endregion

            #region Assert (Afirmação)
            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(Resource.PublicoAlvoInvalido);
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
