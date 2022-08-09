using CursoOnline.Dominio._Base;

namespace CursoOnline.Dominio.Domain
{
    public interface IAlunoRepositorio : IRepositorio<Aluno>
    {
        Aluno ObterPeloCpf(string cpf);
    }
}
