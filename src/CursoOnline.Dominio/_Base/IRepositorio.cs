using System.Collections.Generic;

namespace CursoOnline.Dominio._Base
{
    public interface IRepositorio<TEntidade>
    {
        #region Métodos
        TEntidade ObterPorId(int id);
        List<TEntidade> Consultar();
        void Adicionar(TEntidade entity);
        #endregion
    }
}
