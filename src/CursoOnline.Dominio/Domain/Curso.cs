using CursoOnline.Dominio._Base;
using System;

namespace CursoOnline.Dominio.Domain
{
    public class Curso : Entidade
    {
        #region Atributos
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        #endregion

        #region Construtores
        private Curso() { }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), "Nome Inválido.")
                .Quando(cargaHoraria < 1, "Carga horária deve ser maior que 1 hora.")
                .Quando(valor < 1, "Valor deve ser maior que R$1,00.")
                .DispararExcecaoSeExistir();

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public void AlterarNome(string nome)
        {
            ValidadorDeRegra.Novo()
                .Quando(string.IsNullOrEmpty(nome), "Nome Inválido.")
                .DispararExcecaoSeExistir();

            Nome = nome;
        }

        public void AlterarCargaHoraria(double cargaHoraria)
        {
            ValidadorDeRegra.Novo()
                .Quando(cargaHoraria < 1, "Carga horária deve ser maior que 1 hora.")
                .DispararExcecaoSeExistir();

            CargaHoraria = cargaHoraria;
        }

        public void AlterarValor(double valor)
        {
            ValidadorDeRegra.Novo()
                .Quando(valor < 1, "Valor deve ser maior que R$1,00.")
                .DispararExcecaoSeExistir();

            Valor = valor;
        }
        #endregion
    }
}
