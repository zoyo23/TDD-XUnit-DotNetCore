using System;

namespace CursoOnline.Dominio.Domain
{
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
            if (!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out PublicoAlvo publicoAlvo))
            {
                throw new ArgumentException("Público Alvo inválido.");
            }

            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            if (cursoJaSalvo != null)
            {
                throw new ArgumentException("Nome do Curso já consta no banco de dados.");
            }

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor);

            _cursoRepositorio.Adicionar(curso);
        }
        #endregion
    }
}
