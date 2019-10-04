using System;
using Xunit;

namespace CursoOnline.Dominio.Test._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message.Equals(mensagem))
            {
                Assert.True(true, $"Mensagem Esperada: '{mensagem}' | Mensagem Recebida: '{exception.Message}'");
            }
            else
            {
                Assert.False(true, $"Mensagem Esperada: '{mensagem}' | Mensagem Recebida: '{exception.Message}'");
            }
        }
    }
}
