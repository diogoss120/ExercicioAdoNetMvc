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

        //[HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {
            /* Forma adicional de fazer validações
            if (string.IsNullOrEmpty(aluno.Nome))
            {
                ModelState.AddModelError("Nome", "Nome inválido");
            }
            if (string.IsNullOrEmpty(aluno.Email))
            {
                ModelState.AddModelError("Email", "Email inválido");
            }
            if (string.IsNullOrEmpty(aluno.Sexo))
            {
                ModelState.AddModelError("Sexo", "Sexo inválido");
            }
            */
            
            if (aluno.Nascimento >= DateTime.Now.AddYears(-18) )
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
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
