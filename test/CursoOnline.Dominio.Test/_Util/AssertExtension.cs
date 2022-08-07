using CursoOnline.Dominio._Base;
using Xunit;

namespace CursoOnline.Dominio.Test._Util
{
    public static class AssertExtension
    {
        #region Métodos
        public static void ComMensagem(this ExcecaoDeDominio exception, string mensagem)
        {
            string complementoAssert = $@"
Mensagem Esperada: '{mensagem}'
Mensagem Recebida: '{exception.Message}'";

            if (exception.MensagensDeErro.Contains(mensagem))
            {
                Assert.True(true, complementoAssert);
            }
            else
            {
                Assert.False(true, complementoAssert);
            }
        }
        #endregion
    }
}
