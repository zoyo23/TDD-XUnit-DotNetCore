namespace CursoOnline.Dominio.Cursos
{
    public interface ICursoRepositorio
    {
        #region Métodos
        void Adicionar(Curso curso);
        Curso ObterPeloNome(string nome);
        Curso ObterPorId(int id);
        #endregion
    }
}
