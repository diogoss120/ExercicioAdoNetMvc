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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            AlunoBLL aluno = new AlunoBLL();
            List<Aluno> alunos = aluno.GetAlunos();
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
                AlunoBLL _aluno = new AlunoBLL();
                _aluno.InserirAluno(aluno);
                return RedirectToAction("Index");
            }
            ViewBag.Error = "Preencha todos os campos!";
            return View(aluno);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            AlunoBLL aluno = new AlunoBLL();
            Aluno _aluno = aluno.GetAlunos().Single(a => a.Id == Id);
            return View(_aluno);
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
                AlunoBLL _aluno = new AlunoBLL();
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
