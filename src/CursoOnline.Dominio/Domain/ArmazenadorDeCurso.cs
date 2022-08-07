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
                .Quando(!Enum.TryParse<PublicoAlvo>(cursoDto.PublicoAlvo, out PublicoAlvo publicoAlvo), Resource.PublicoAlvoInvalido)
                .Quando(cursoJaSalvo != null, Resource.NomeCursoJaExiste)
                .DispararExcecaoSeExistir();

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, publicoAlvo, cursoDto.Valor);

            if (cursoDto.Id > 0)
            {
                curso = _cursoRepositorio.ObterPorId(cursoDto.Id);

                curso.AlterarNome(cursoDto.Nome);
                curso.AlterarValor(cursoDto.Valor);
                curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            }

            if (cursoDto.Id == 0)
            {
                _cursoRepositorio.Adicionar(curso);
            }
        }
        #endregion
    }
}
