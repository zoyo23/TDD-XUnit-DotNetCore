using CursoOnline.Dominio.Domain;

namespace CursoOnline.Dominio.Test._Builders
{
    public class CursoBuilder
    {
        #region Atributos
        private string _nome = "Informática Básica";
        private double _cargaHoraria = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950;
        private string _descricao = "Uma descrição";
        #endregion

        #region Métodos
        public static CursoBuilder Novo() => new CursoBuilder();

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public CursoBuilder ComCargaPublicoAlvo(PublicoAlvo publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }
        #endregion
    }
}
