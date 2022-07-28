using System.Diagnostics.CodeAnalysis;

namespace CursoOnline.Dominio._Base
{
    [ExcludeFromCodeCoverage]
    public abstract class Entidade
    {
        #region Atributos
        public int Id { get; protected set; }
        #endregion
    }
}
