using CursoOnline.Dominio.Domain;
using Moq;
using Xunit;

namespace CursoOnline.Dominio.Test.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        #region Testes
        [Fact]
        public void DeveAdicionarCurso()
        {
            #region Arrange
            var cursoDto = new CursoDto
            {
                Nome = "Curso A",
                Descricao = "Descrição",
                CargaHoraria = 80,
                PublicoAlvo = 1,
                Valor = 850.00
            };

            var cursoRepositoryMock = new Mock<ICursoRepositorio>();

            var armazenadorDeCurso = new ArmazenadorDeCurso(cursoRepositoryMock.Object);
            #endregion

            #region Act
            armazenadorDeCurso.Armazenar(cursoDto);
            #endregion

            #region Assert
            cursoRepositoryMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
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
        public int CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
        #endregion
    }
}
