using CursoOnline.Dominio._Base;
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
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out PublicoAlvo publicoAlvo), "Público Alvo inválido.")
                .Quando(cursoJaSalvo != null, "Nome do Curso já consta no banco de dados.")
                .DispararExcecaoSeExistir();

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor);

            _cursoRepositorio.Adicionar(curso);
        }
        #endregion
    }
}
