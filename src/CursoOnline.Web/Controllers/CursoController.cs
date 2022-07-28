using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Domain;
using CursoOnline.Web.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CursoOnline.Web.Controllers
{
    public class CursoController : Controller
    {
        #region Atributos
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        #endregion

        #region Construtores
        public CursoController(ArmazenadorDeCurso armazenadorDeCurso)
        {
            _armazenadorDeCurso = armazenadorDeCurso;
        }
        #endregion

        public IActionResult Index()
        {
            var cursos = new List<CursoParaListagemDto>();
            return View("Index", PaginatedList<CursoParaListagemDto>.Create(cursos, Request));
        }

        public IActionResult Novo()
        {
            return View("NovoOuEditar", new CursoDto());
        }

        [HttpPost]
        public IActionResult Salvar(CursoDto model)
        {
            _armazenadorDeCurso.Armazenar(model);
            return Ok();
        }
    }
}
