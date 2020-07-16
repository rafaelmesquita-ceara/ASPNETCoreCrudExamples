using System.Collections.Generic; 
using System.Threading.Tasks; // Trabalhando de forma assíncrona
using Microsoft.AspNetCore.Mvc; // Trabalhando com o padrão MVC do aspnetcore
using Microsoft.EntityFrameworkCore; // Importando a biblioteca EntityFramework
using aspnetcoreapi.Data; // Importando meu DbContext
using aspnetcoreapi.Models; // Importando meus models

namespace aspnetcoreapi.Controllers
{
    [ApiController] // Especificando o controller para API (e não para razor pages)
    // Como não especifiquei um roteamento no startup, ele vai se mapear pelo controller
    [Route("v1/categories")]  // v1 para versionamento, no caminho /categories
    public class CategoryController : ControllerBase
    {

      [HttpGet] // Método de requisição GET
      [Route("")] // Rota padrão (/)
      public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context) 
      // Task para forma assincrona, irei retornar uma lista de categorias, irei pegar o DataContext dos services, onde já defini o DataContext
      {
        var categories = await context.Categories.Include(b => b.ProductCategories).ThenInclude(x => x.Product).ToListAsync(); // Incluo minhas categorias do produto.ToListAsync(); // Faço uma consulta no meu database para pegar todas as categorias
        return categories; // Retorno as categorias
      }


      [HttpPost] // Método de requisição Post
      [Route("")] // Rota padrão (/)
      public async Task<ActionResult<Category>> Post(
        [FromServices] DataContext context, // Pego o DataContext dos services no Startup.cs
        [FromBody]Category model) // Pego o modelo Category do corpo da requisição
      {
        // Valido minha categoria com base no Model Category.cs
        if (ModelState.IsValid)   
        {
          //  Caso todos a validacao tenha sido bem sucedida, ele salva no meu banco de dados a Categoria
          context.Categories.Add(model);
          await context.SaveChangesAsync();
          return model; // Retorno meu modelo Category
        }
        else
        {
          // Caso a validacao nao tenha sido bem sucedida, ele retorna o erro especificado no Model
          return BadRequest(ModelState);
        }
      }
    }
}