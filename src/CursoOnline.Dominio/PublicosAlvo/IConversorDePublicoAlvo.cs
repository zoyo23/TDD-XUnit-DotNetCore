using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public interface IConversorDePublicoAlvo
    {
        PublicoAlvo Converter(string publicoAlvo);
    }
}
