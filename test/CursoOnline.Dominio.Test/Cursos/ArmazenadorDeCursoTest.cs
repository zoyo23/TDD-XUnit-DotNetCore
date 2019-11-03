using Bogus;
using CursoOnline.Dominio.Domain;
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
                PublicoAlvo = 1,
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
        #endregion
    }

    public interface ICursoRepositorio
    {
        #region Métodos
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
        #endregion
    }

    public class ArmazenadorDeCurso
    {
        #region Atributos
        private readonly ICursoRepositorio _cursoRepositorio;
        #endregion

        #region Construtores
        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }
        #endregion

        #region Métodos
        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, PublicoAlvo.Estudante, cursoDto.Valor);

            _cursoRepositorio.Adicionar(curso);
        }
        #endregion
    }

    public class CursoDto
    {
        #region Atributos
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
        #endregion
    }
}
