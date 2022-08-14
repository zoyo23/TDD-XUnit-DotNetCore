using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.PublicosAlvo;
using System;

namespace CursoOnline.Dominio.Test._Builders
{
    public class AlunoBuilder
    {
        #region Atributos
        private string _nome = "Teles Cristiano";
        private string _cpf = "527.676.215-59";
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private string _email = "teste@teste.com.br";
        private int _id = 0;
        #endregion

        #region Métodos
        public static AlunoBuilder Novo()
        {
            return new AlunoBuilder();
        }

        public Aluno Build()
        {
            var aluno = new Aluno(_nome, _email, _cpf, _publicoAlvo);

            if (_id > 0)
            {
                var propertyInfo = aluno.GetType().GetProperty("Id");
                propertyInfo.SetValue(aluno, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
            }

            return aluno;
        }

        public AlunoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public AlunoBuilder ComEmail(string email)
        {
            _email = email;
            return this;
        }

        public AlunoBuilder ComCpf(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public AlunoBuilder ComCargaPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        internal AlunoBuilder ComId(int id)
        {
            _id = id;
            return this;
        }
        #endregion
    }
}
