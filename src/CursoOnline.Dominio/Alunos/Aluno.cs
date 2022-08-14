﻿using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System.Text.RegularExpressions;

namespace CursoOnline.Dominio.Alunos
{
    public class Aluno : Entidade
    {
        #region Propriedades
        private readonly Regex _emailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex _cpfRegex = new Regex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        #endregion

        #region Construtores
        private Aluno() { }

        public Aluno(string nome, string email, string cpf, PublicoAlvo publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .Quando(string.IsNullOrEmpty(email) || !_emailRegex.Match(email).Success, Resource.EmailInvalido)
                .Quando(string.IsNullOrEmpty(cpf) || !_cpfRegex.Match(cpf).Success, Resource.CpfInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
            Email = email;
            Cpf = cpf;
            PublicoAlvo = publicoAlvo;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), Resource.NomeInvalido)
                .DispararExcecaoSeExistir();

            Nome = nome;
        }
        #endregion

        #region Métodos

        #region Métodos Públicos

        #endregion

        #endregion
    }
}
