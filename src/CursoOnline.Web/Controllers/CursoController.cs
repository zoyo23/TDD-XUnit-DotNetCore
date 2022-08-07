using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Domain;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        #region Atributos
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly IRepositorio<Curso> _cursoRepositorio;
        #endregion

        #region Construtores
        public CursoController(ArmazenadorDeCurso armazenadorDeCurso, IRepositorio<Curso> cursoRepositorio)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
            _cursoRepositorio = cursoRepositorio;
        }
        #endregion

        public IActionResult Index()
        {
            var cursos = _cursoRepositorio.Consultar();

            if (cursos.Any())
            {
                var dtos = cursos.Select(c => new CursoParaListagemDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    CargaHoraria = c.CargaHoraria,
                    PublicoAlvo = c.PublicoAlvo.ToString(),
                    Valor = c.Valor
                });

                return View("Index", PaginatedList<CursoParaListagemDto>.Create(dtos, Request));
            }

            return View("Index", PaginatedList<CursoParaListagemDto>.Create(null, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        public IActionResult Editar(int id)
        {
            var curso = _cursoRepositorio.ObterPorId(id);

            var cursoDto = new CursoDto
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria,
                PublicoAlvo = curso.PublicoAlvo.ToString(),
                Valor = curso.Valor
            };

            return View("NovoOuEditar", cursoDto);
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Ok();
        }
    }
}
