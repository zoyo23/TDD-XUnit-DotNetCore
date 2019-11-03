namespace CursoOnline.Dominio.Domain
{
    public interface ICursoRepositorio
    {
        #region Métodos
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
        #endregion
    }
}
