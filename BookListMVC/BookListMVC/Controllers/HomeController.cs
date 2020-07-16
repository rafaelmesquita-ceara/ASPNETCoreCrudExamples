using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookListMVC.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BookListMVC.Controllers
{
    public class HomeController : Controller
    {
        // Aqui estou passando meu DbContext aqui para ser utilizado por todo o meu controller quando precisar
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public User User { get; set; }
        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? id)
        {
            if (HttpContext.Session.GetString("SessionUser") == null) // Verifico se existe um usuario na minha sessão
            {
                return View();  // Se não existir, redireciono para o index (tela de login)
            }
            return RedirectToAction("Index", "Books", new { area = "" });

        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
