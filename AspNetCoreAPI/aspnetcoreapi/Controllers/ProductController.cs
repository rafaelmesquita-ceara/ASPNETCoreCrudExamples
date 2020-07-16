using System.Collections.Generic; 
using System.Threading.Tasks; // Trabalhando de forma assíncrona
using Microsoft.AspNetCore.Mvc; // Trabalhando com o padrão MVC do aspnetcore
using Microsoft.EntityFrameworkCore; // Importando a biblioteca EntityFramework
using aspnetcoreapi.Data; // Importando meu DbContext
using aspnetcoreapi.Models; // Importando meus models
using System.Linq;

namespace aspnetcoreapi.Controllers
{
    [ApiController] // Especificando o controller para API (e não para razor pages)
    // Como não especifiquei um roteamento no startup, ele vai se mapear pelo controller
    [Route("v1/products")]  // v1 para versionamento, no caminho /products
    public class ProductController : ControllerBase
    {
      #region GET
      [HttpGet] // Método de requisição GET
      [Route("")] // Rota padrão (/)
      public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context) 
      // Task para forma assincrona, irei retornar uma lista de produtos, irei pegar o DataContext dos services, onde já defini o DataContext
      {
        var products = await context.Products.Include(b => b.ProductCategories).ThenInclude(x => x.Category) // Incluo minhas categorias do produto
        .ToListAsync(); // Faço uma consulta no meu database para pegar todos os produtos
        return products; // Retorno os produtos
      }

      [HttpGet] // Método de requisição GET
      [Route("{id:int}")] // Recebo meu id de produto
      public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)  // no final recebo um int id para referenciar o id recebido na rota
      // Task para forma assincrona, irei retornar um produto com base no ID, irei pegar o DataContext dos services, onde já defini o DataContext
      {
        var product = await context.Products
          .AsNoTracking() // Para nao criar proxys do meu objeto
          .Include(b => b.ProductCategories).ThenInclude(x => x.Category) // Incluo minhas categorias do produto
          .FirstOrDefaultAsync(x => x.Id == id); // Recebo o produto com base no id
        return product; // Retorno o produto
      }

      [HttpGet] // Método de requisição GET
      [Route("categories/{id:int}")] // Recebo meu id de categoria
      public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)  // no final recebo um int id para referenciar o id recebido na rota
      // Task para forma assincrona, irei retornar produtos com base na categoria, irei pegar o DataContext dos services, onde já defini o DataContext
      {
        var products = await context.Products
        .Where(p => p.ProductCategories
        .Any(c => c.Category.Id == id))
        .ToListAsync();

        return products; // Retorno os produtos
      }
      #endregion
      #region POST
      [HttpPost] // Método de requisição Post
      [Route("")] // Rota padrão (/)
      public async Task<ActionResult<Product>> Post(
        [FromServices] DataContext context, // Pego o DataContext dos services no Startup.cs
        [FromBody]Product model) // Pego o modelo Product do corpo da requisição
      {
        // Valido meu produto com base no Model Product.cs
        if (ModelState.IsValid)   
        {
          var productCategories = new List<ProductCategory>(); // Crio uma lista separada do meu model para nao gerar conflito na hora de inserir no banco de dados
          productCategories = model.ProductCategories.ToList(); // Preciso converter a Collection para List
          model.ProductCategories = null; // Deixo o antigo ProductCategories no meu model nulo para nao gerar conflito
          //  Caso todos a validacao tenha sido bem sucedida, ele salva no meu banco de dados o Produto
          context.Products.Add(model);
          await context.SaveChangesAsync();

          foreach (ProductCategory p in productCategories)  // Percorro minha lista de productCategories passada pelo body
          {
            p.ProductId = model.Id; // Defino o ProductId no Id retornado do Produto recem criado
            context.ProductCategories.Add(p); // Adiciono minha ProductCategory
            await context.SaveChangesAsync(); // Salvo as mudancas
          }
          return model; // Retorno meu modelo Product
        }
        else
        {
          // Caso a validacao nao tenha sido bem sucedida, ele retorna o erro especificado no Model
          return BadRequest(ModelState);
        }
      }
      #endregion
      #region UPDATE
      [HttpPut] // Método de requisição Put
      [Route("{id:int}")] // Rota padrão (/)
      public async Task<ActionResult<Product>> Update(
        [FromServices] DataContext context, // Pego o DataContext dos services no Startup.cs
        [FromBody]Product model, // Pego o modelo Product do corpo da requisição
        int id)  // Uso o id passado pela rota
      {
        // Valido meu produto com base no Model Product.cs
        if (ModelState.IsValid)   
        {
          var result = context.Products.SingleOrDefault(x => x.Id == id); // Procuro no meu database o produto com o id especificado
          if (result != null)
          {
            // Caso ele ache, vou modificar campo por campo, substituindo pelo model passado no body
              result.Title = model.Title;
              result.Description = model.Description;
              result.Price = model.Price;
              await context.SaveChangesAsync(); // Salvo no meu database o result modificado
              return result; // Retorno meu Result
              
          }
          return StatusCode(400, "Produto nao encontrado"); // Caso ele nao ache o produto, ele vai dar bad request
        }
        else
        {
          // Caso a validacao nao tenha sido bem sucedida, ele retorna o erro especificado no Model
          return BadRequest(ModelState);
        }
      }
      #endregion
      #region DELETE
      [HttpDelete] // Método de requisição Delete
      [Route("{id:int}")] // Rota padrão (/)
      public async Task<ActionResult<Product>> Delete(
        [FromServices] DataContext context, // Pego o DataContext dos services no Startup.cs
        int id)  // Uso o id passado pela rota
      {
        // Valido meu produto com base no Model Product.cs
          var result = context.Products.SingleOrDefault(x => x.Id == id);
          if (result != null)
          {
              context.Products.Attach(result); // Pego meu result no banco
              context.Products.Remove(result); // Deleto meu result no banco
              await context.SaveChangesAsync(); // Salvo as alteracoes de forma assincrona
              return StatusCode(200); // Retorno um status code 200 (sucesso)
          }
          return StatusCode(400, "Produto nao encontrado"); // Caso eu nao ache o produto, retorno o status 400 de badrequest
      }
      #endregion
    }
}


