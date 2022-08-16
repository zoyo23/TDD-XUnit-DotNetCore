using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;

namespace CursoOnline.Dominio.Matriculas
{
    public class Matricula : Entidade
    {
        #region Atributos
        public Aluno Aluno { get; private set; }
        public Curso Curso { get; private set; }
        public double ValorPago { get; private set; }
        public bool TemDesconto { get; private set; }
        public double NotaDoAluno { get; private set; }
        public bool CursoConcluido { get; private set; }
        public bool Cancelada { get; private set; }
        #endregion

        #region Construtores
        public Matricula(Aluno aluno, Curso curso, double valorPago)
        {
            ValidadorDeRegra.Novo()
                .Quando((aluno == null), Resource.AlunoInvalido)
                .Quando((curso == null), Resource.CursoInvalido)
                .Quando((valorPago < 1), Resource.ValorPagoInvalido)
                .Quando((curso != null && valorPago > curso.Valor), Resource.ValorPagoMaiorQueValorDoCurso)
                .Quando((curso != null && aluno != null && aluno.PublicoAlvo != curso.PublicoAlvo), Resource.PublicosAlvoDiferentes)
                .DispararExcecaoSeExistir();

            Aluno = aluno;
            Curso = curso;
            ValorPago = valorPago;
            TemDesconto = valorPago < curso.Valor;
        }
        #endregion

        #region Mètodos

        #region Métodos Públicos
        public void InformarNota(double notaDoAluno)
        {
            ValidadorDeRegra.Novo()
                .Quando((notaDoAluno < 0 || notaDoAluno > 10), Resource.NotaDoAlunoInvalida)
                .Quando((Cancelada), Resource.MatriculaCancelada)
                .DispararExcecaoSeExistir();

            NotaDoAluno = notaDoAluno;
            CursoConcluido = true;
        }

        public void Cancelar()
        {
            ValidadorDeRegra.Novo()
                .Quando((CursoConcluido), Resource.MatriculaConcluida)
                .DispararExcecaoSeExistir();

            Cancelada = true;
        }
        #endregion

        #endregion
    }
}
