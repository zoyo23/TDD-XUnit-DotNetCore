using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.Dominio.Test._Util;
using System;
using Xunit;

namespace CursoOnline.Dominio.Test.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        #region Atributos
        private readonly ConversorDePublicoAlvo _conversorDePublicoAlvo = new ConversorDePublicoAlvo();
        #endregion

        #region Construtores

        #endregion

        #region Testes
        [Theory]
        [InlineData(PublicoAlvo.Empregado, "Empregado")]
        [InlineData(PublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(PublicoAlvo.Estudante, "Estudante")]
        [InlineData(PublicoAlvo.Universitario, "Universitario")]
        public void DeveConverterPublicAlvo(PublicoAlvo publicosAlvoEsperado, string nomePublicoAlvoEsperado)
        {
            #region Arrange
            #endregion

            #region Act
            var publicoAlvoConvertido = _conversorDePublicoAlvo.Converter(nomePublicoAlvoEsperado);
            #endregion

            #region Assert
            Assert.Equal(publicosAlvoEsperado, publicoAlvoConvertido);
            #endregion
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoInvalido()
        {
            #region Arrange
            const string publicoAlvoInvalido = "Inválido";
            #endregion

            #region Act
            Action act = () => _conversorDePublicoAlvo.Converter(publicoAlvoInvalido);
            #endregion

            #region Assert
            Assert.Throws<ExcecaoDeDominio>(act)
                .ComMensagem(Resource.PublicoAlvoInvalido);
            #endregion
        }
        #endregion
    }
}
