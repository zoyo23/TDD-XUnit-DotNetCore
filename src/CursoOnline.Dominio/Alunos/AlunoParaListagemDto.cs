using System.Diagnostics.CodeAnalysis;

namespace CursoOnline.Dominio.Alunos
{
    [ExcludeFromCodeCoverage]
    public class AlunoParaListagemDto
    {
        #region Atributos
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string PublicoAlvo { get; set; }
        #endregion
    }
}
