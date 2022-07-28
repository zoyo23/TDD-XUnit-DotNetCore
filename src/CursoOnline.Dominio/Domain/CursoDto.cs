﻿using System.Diagnostics.CodeAnalysis;

namespace CursoOnline.Dominio.Domain
{
    [ExcludeFromCodeCoverage]
    public class CursoDto
    {
        #region Atributos
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
        public int Id { get; set; }
        #endregion
    }
}
