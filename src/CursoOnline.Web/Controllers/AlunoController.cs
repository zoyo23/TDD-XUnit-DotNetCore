using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class AlunoController : Controller
    {
        #region Atributos
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly IRepositorio<Aluno> _alunoRepositorio;
        #endregion

        #region Construtores
        public AlunoController(ArmazenadorDeAluno armazenadorDeAluno, IRepositorio<Aluno> alunoRepositorio)
        {
            _armazenadorDeAluno = armazenadorDeAluno;
            _alunoRepositorio = alunoRepositorio;
        }
        #endregion

        #region Metódos

        #region Métodos Públicos
        public IActionResult Index()
        {
            var alunos = _alunoRepositorio.Consultar();

            if (alunos.Any())
            {
                var dtos = alunos.Select(a => new AlunoParaListagemDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Cpf = a.Cpf,
                    Email = a.Email
                });

                return View("Index", PaginatedList<AlunoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<AlunoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunoRepositorio.ObterPorId(id);

            var dto = new AlunoDto
            {
                Id = aluno.Id,
                Nome = aluno.Nome,
                Cpf = aluno.Cpf,
                Email = aluno.Email,
                PublicoAlvo = aluno.PublicoAlvo.ToString()
            };

            return View("NovoOuEditar", dto);
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new AlunoDto());
        }

        public IActionResult Salvar(AlunoDto model)
        {
            _armazenadorDeAluno.Armazenar(model);
            return Ok();
        }
        #endregion

        #region Métodos Privados

        #endregion

        #endregion
    }
}
