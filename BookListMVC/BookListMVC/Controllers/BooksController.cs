using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        // Aqui estou passando meu DbContext aqui para ser utilizado por todo o meu controller quando precisar
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Book Book { get; set; }
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("SessionUser") == null) // Verifico se existe um usuario na minha sessão
            {
                return RedirectToAction("Index", "Home", new { area = "" });    // Se não existir, redireciono para o index (tela de login)
            }
            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("SessionUser")); // Se existir, converto o json para o objeto User 
            ViewData["Username"] = sessionUser.CH_Name; // E passo o nome para a view
            return View();  // Retorno a view
            
        }


        // Rota para criacao de livros
        public IActionResult Upsert(int? id)
        {
            Book = new Book();
            
            // Verifico se foi passado algum id pela rota 
            if (id == null)
            {
                //create
                return View(Book);  // Se não, ele vai retornar o modelo vazio para a view
            }
            //update
            Book = _db.Books.FirstOrDefault(u => u.Id == id);   // Se sim, ele vai buscar esse id no banco de dados
            if (Book == null)
            {
                return NotFound();  // caso nao exista o livro no banco de dados, ele retorna NotFound
            }
            return View(Book);  // Caso exista, ele vai retornar a View com o modelo Book preenchido pela busca
        }

        #region API Calls
        [HttpPost]  // Rota padrão (/) no método POST
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid) // Valido meus dados com base no modelo
            {
                var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("SessionUser")); // Recebo meu usuario logado na sessao
                Book.UserID = sessionUser.Id; // O UserId do livro sera o Id do meu usuario logado na sessao
                if (Book.Id == 0) // Se o ID passado pela view for 0 (caso ele tenha colocado um ID na rota esse id sera diferente de 0), ele vai criar um livro no banco de dados
                {
                    //create
                    _db.Books.Add(Book);
                }
                else    // Se o id for diferente de 0, ele vai atualizar o livro existente no banco de dados
                {
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();  // Salvo minhas alteracoes
                return RedirectToAction("Index");       // Redireciono para a lista de livros
            }
            return View(Book);  // Caso nao tenha sido validado, eu retorno para a mesma pagina com os campos preenchidos com o modelo
        }

        [HttpGet]   // Rota padrao (/) no metodo GET
        public async Task<IActionResult> GetAll()
        {
            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("SessionUser"));
            return Json(new { data = await _db.Books.Where(x => x.UserID == sessionUser.Id).ToListAsync() });  // Aqui retorno uma Lista em formato Json de forma assincrona para ser utilizada pelo AJAX na minha view
        }

        [HttpDelete]    // Rota padrao (/) no metodo DELETE
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);  // Procuro o livro com base no id fornecido na rota
            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" }); // Se o livro nao for achado no banco de dados, eu retorno uma mensagem de erro
            }
            _db.Books.Remove(bookFromDb);   // Se o livro for encontrado, eu removo o livro do banco de dados
            await _db.SaveChangesAsync();   // Salvo as alteracoes de forma assincrona
            return Json(new { success = true, message = "Delete successful" }); // Retorno um Json informando o sucesso
        }
        #endregion
    }
}