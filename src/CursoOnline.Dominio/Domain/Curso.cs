using System;

namespace CursoOnline.Dominio.Domain
{
    public class Curso
    {
        #region Construtores
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome obrigatório.");
            }

            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horária deve ser maior que 1 hora.");
            }

            if (valor < 1)
            {
                throw new ArgumentException("Valor deve ser maior que R$1,00.");
            }

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }
        #endregion

        #region Atributos
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }
        #endregion
    }
}
