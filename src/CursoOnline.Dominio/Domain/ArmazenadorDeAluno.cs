using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System;

namespace CursoOnline.Dominio.Domain
{
    public class ArmazenadorDeAluno
    {
        #region Atributos
        private readonly IAlunoRepositorio _alunoRepositorio;
        private readonly IConversorDePublicoAlvo _conversorDePublicoAlvo;
        #endregion

        #region Construtores
        public ArmazenadorDeAluno(IAlunoRepositorio alunoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _alunoRepositorio = alunoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Armazenar(AlunoDto alunoDto)
        {
            var alunoJaCadastrado = _alunoRepositorio.ObterPeloCpf(alunoDto.Cpf);

            ValidadorDeRegra.Novo()
                .Quando(alunoJaCadastrado != null && alunoJaCadastrado.Id != alunoDto.Id, Resource.CpfJaCadastrado)
                .DispararExcecaoSeExistir();
            
            var publicoAlvo = _conversorDePublicoAlvo.Converter(alunoDto.PublicoAlvo);

            if (alunoDto.Id == 0)
            {
                var aluno = new Aluno(alunoDto.Nome, alunoDto.Email, alunoDto.Cpf, publicoAlvo);
                _alunoRepositorio.Adicionar(aluno);
            }
            else
            {
                var aluno = _alunoRepositorio.ObterPorId(alunoDto.Id);
                aluno.AlterarNome(alunoDto.Nome);
            }
        }
        #endregion
    }
}
