using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookListMVC.Controllers
{
    public class UserController : Controller
    {
        // Aqui estou passando meu DbContext aqui para ser utilizado por todo o meu controller quando precisar
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public new User User{ get; set; }
        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }


        //Rota User/Create vou retornar a pagina de cadastro
        public IActionResult Create(int? id)
        {
            var count = _db.Users.Count();
            ViewBag.Msg = count.ToString();
            return View();
        }


        #region API CALLS
        [HttpPost]  // Post para LOGIN
        [ValidateAntiForgeryToken]
        public IActionResult Login()
        {
            var query = _db.Users
                       .Where(s => s.CH_Login == User.CH_Login && s.CH_Password == User.CH_Password)
                       .FirstOrDefault(); // Procuro se existe um usuario com os dados informados

            if (query != null) // Caso tenha, ele vai armazenar o usuario na sessao e prosseguir para a lista de livros
            {
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(query));   // Forneço meu usuario para uma string da sessão, que seria a SessionUser 
                return RedirectToAction("Index", "Books", new { area = "" }); // Retorno a lista de livros.
            }
            else // Caso não tenha, irei mandar um sinal de falha para uma caixa vermelha aparecer informando que as credenciais estao erradas
            {
                ViewBag.Msg = "fail";
                return RedirectToAction("Index", "Home", new { area = "" }); ;   // Retorno o Index, que no caso seria a pagina inicial do BookListMVC

            }
        }

        [HttpPost]      // Rota de criacao de usuario no banco de dados
        [ValidateAntiForgeryToken]
        public IActionResult Create()
        {
            if (ModelState.IsValid) // Valido meus dados com base no modelo
            {
                //create
                _db.Users.Add(User);        // Adiciono meu User passado pelo formulario da pagina de cadastro ao banco de dados
                _db.SaveChanges();          // Salvo as alteracoes
                HttpContext.Session.SetString("SessionUser", JsonConvert.SerializeObject(User));   // Forneço meu usuario para uma string da sessão, que seria a SessionUser 
                return RedirectToAction("Index", "Books", new { area = "" });   // Redireciono para a Lista de livros
            }
            return View("Create");
        }

        [HttpGet]   // Rota padrao (/) no metodo GET
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();  // Limpo minha sessão
            return RedirectToAction("Index", "Home", new { area = "" }); // Redireciono para a Home
        }
        #endregion

    }
}