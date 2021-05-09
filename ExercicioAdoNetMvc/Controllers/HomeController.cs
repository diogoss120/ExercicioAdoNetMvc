using ExercicioAdoNetMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExercicioAdoNetMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAluno _aluno;
        public HomeController(IAluno aluno, ILogger<HomeController> logger)
        {
            _logger = logger;
            _aluno = aluno;
        }

        public IActionResult Index()
        {
            List<Aluno> alunos = _aluno.GetAlunos();
            return View("Lista", alunos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            if (aluno.Nascimento >= DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("Nascimento", "Aluno menor de 18 anos");
            }

            if (ModelState.IsValid)
            {
                _aluno.InserirAluno(aluno);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Preencha todos os campos!";
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Aluno aluno = _aluno.GetAlunos().Single(a => a.Id == Id);
            return View(aluno);
        }

        [HttpPost]
        public IActionResult Edit(Aluno aluno)
        {

            if (aluno.Nascimento >= DateTime.Now.AddYears(-18))
            {
                ModelState.AddModelError("Nascimento", "Aluno menor de 18 anos");
            }

            if (ModelState.IsValid)
            {
                _aluno.AtualizarAluno(aluno);
                return RedirectToAction("Index");
            }
            return View(aluno);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
