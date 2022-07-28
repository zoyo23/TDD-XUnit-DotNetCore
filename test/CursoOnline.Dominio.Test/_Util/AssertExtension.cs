using System;
using Xunit;

namespace CursoOnline.Dominio.Test._Util
{
    public static class AssertExtension
    {
        #region Métodos
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            string complementoAssert = $@"
Mensagem Esperada: '{mensagem}'
Mensagem Recebida: '{exception.Message}'";

            if (exception.Message.Equals(mensagem))
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
