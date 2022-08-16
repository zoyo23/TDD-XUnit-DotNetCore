using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Matriculas;

namespace CursoOnline.Dominio.Test._Builders
{
    internal class MatriculaBuilder
    {
        #region Atributos
        protected Aluno Aluno;
        protected Curso Curso;
        protected double ValorPago;
        protected bool Cancelada;
        protected bool Concluido;
        #endregion

        #region Métodos
        public static MatriculaBuilder Novo()
        {
            var curso = CursoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empreendedor).Build();

            return new MatriculaBuilder
            {
                Aluno = AlunoBuilder.Novo().ComPublicoAlvo(Dominio.PublicosAlvo.PublicoAlvo.Empreendedor).Build(),
                Curso = curso,
                ValorPago = curso.Valor
            };
        }

        public Matricula Build()
        {
            var matricula = new Matricula(Aluno, Curso, ValorPago);

            if (Cancelada)
            {
                matricula.Cancelar();
            }

            if (Concluido)
            {
                var notaDoAluno = 7;
                matricula.InformarNota(notaDoAluno);
            }

            return matricula;
        }

        public MatriculaBuilder ComAluno(Aluno aluno)
        {
            Aluno = aluno;
            return this;
        }

        public MatriculaBuilder ComCurso(Curso curso)
        {
            Curso = curso;
            return this;
        }

        public MatriculaBuilder ComValorPago(double valorPago)
        {
            ValorPago = valorPago;
            return this;
        }

        internal MatriculaBuilder ComCancelada(bool cancelada)
        {
            Cancelada = cancelada;
            return this;
        }

        internal MatriculaBuilder ComConcluido(bool concluido)
        {
            Concluido = concluido;
            return this;
        }
        #endregion
    }
}
