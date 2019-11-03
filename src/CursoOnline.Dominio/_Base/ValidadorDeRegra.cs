using System;
using System.Collections.Generic;
using System.Linq;

namespace CursoOnline.Dominio._Base
{
    public class ValidadorDeRegra
    {
        #region Atributos
        private readonly List<string> _mensagensDeErros;
        #endregion

        #region Métodos
        #region Métodos Públicos
        public static ValidadorDeRegra Novo()
        {
            return new ValidadorDeRegra();
        }

        public ValidadorDeRegra Quando(bool temErro, string mensagemDeErro)
        {
            if (temErro)
                _mensagensDeErros.Add(mensagemDeErro);

            return this;
        }

        public void DispararExcecaoSeExistir()
        {
            if (_mensagensDeErros.Any())
                throw new ExcecaoDeDominio(_mensagensDeErros);
        }
        #endregion

        #region Métodos Privados
        private ValidadorDeRegra()
        {
            _mensagensDeErros = new List<string>();
        }
        #endregion 
        #endregion
    }

    public class ExcecaoDeDominio : ArgumentException
    {
        public List<string> MensagensDeErro { get; set; }

        public ExcecaoDeDominio(List<string> mensagensDeErros)
        {
            MensagensDeErro = mensagensDeErros;
        }
    }
}
