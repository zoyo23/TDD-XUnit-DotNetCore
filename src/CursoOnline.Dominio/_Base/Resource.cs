namespace CursoOnline.Dominio._Base
{
    public static class Resource
    {

        public static string EmailInvalido { get; private set; } = "Email Inválido.";
        public static string CpfInvalido { get; private set; } = "CPF Inválido.";
        public static string CpfJaCadastrado { get; private set; } = "CPF já cadastrado anteriormente.";
        public static string NomeInvalido { get; private set; } = "Nome Inválido.";
        public static string CargaHorariaInvalida { get; private set; } = "Carga horária deve ser maior que 1 hora.";
        public static string ValorInvalido { get; private set; } = "Valor deve ser maior que R$1,00.";
        public static string PublicoAlvoInvalido { get; private set; } = "Público Alvo inválido.";
        public static string NomeCursoJaExiste { get; private set; } = "Nome do Curso já consta no banco de dados.";
        public static string AlunoInvalido { get; private set; } = "Aluno Inválido.";
        public static string CursoInvalido { get; private set; } = "Curso Inválido.";
        public static string ValorPagoInvalido { get; private set; } = "Valor Pago Inválido.";
        public static string ValorPagoMaiorQueValorDoCurso { get; private set; } = "Valor Pago Maior Que Valor do Curso.";
        public static string PublicosAlvoDiferentes { get; private set; } = "Publicos Alvos Diferentes.";
        public static string CursoNaoEncontrado { get; private set; } = "Curso não encontrado.";
        public static string AlunoNaoEncontrado { get; private set; } = "Aluno não encontrado.";
        public static string NotaDoAlunoInvalida { get; private set; } = "Nota Do Aluno Invalida.";
        public static string MatriculaNaoEncontrada { get; private set; } = "Matricula não encontrada.";
        public static string MatriculaCancelada { get; private set; } = "Matricula já cancelada.";
        public static string MatriculaConcluida { get; private set; } = "Matricula já concluída.";
    }
}
