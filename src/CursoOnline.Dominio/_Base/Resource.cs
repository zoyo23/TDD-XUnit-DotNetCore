namespace CursoOnline.Dominio._Base
{
    public static class Resource
    {
        public static string EmailInvalido { get; set; } = "Email Inválido.";
        public static string CpfInvalido { get; set; } = "CPF Inválido.";
        public static string NomeInvalido { get; internal set; } = "Nome Inválido.";
        public static string CargaHorariaInvalida { get; internal set; } = "Carga horária deve ser maior que 1 hora..";
        public static string ValorInvalido { get; internal set; } = "Valor deve ser maior que R$1,00..";
        public static string PublicoAlvoInvalido { get; internal set; } = "Público Alvo inválido.;;";
        public static string NomeCursoJaExiste { get; internal set; } = "Nome do Curso já consta no banco de dados.;;";
    }
}
