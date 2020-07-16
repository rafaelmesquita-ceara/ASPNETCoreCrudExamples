using Microsoft.EntityFrameworkCore;
using aspnetcoreapi.Models;

namespace aspnetcoreapi.Data
{
  public class DataContext : DbContext
  {
    // Crio meu DbContext (string de conexão no Startup.cs)
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    // Aqui mapeio todos os meus models que vão ser trabalhados no banco de dados
    public DbSet<Product> Products {get; set; }
    public DbSet<Category> Categories {get; set; }
    public DbSet<ProductCategory> ProductCategories {get; set;}
  }
}