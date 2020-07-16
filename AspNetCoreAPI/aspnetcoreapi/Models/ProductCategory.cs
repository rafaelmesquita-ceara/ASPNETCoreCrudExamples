using System.ComponentModel.DataAnnotations;

namespace aspnetcoreapi.Models
{
  public class ProductCategory
  {
    [Key]
    public int Id {get; set;}
    public int ProductId {get; set;}
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Category Category {get; set;}
  }
    public class CategoriesId
    {
        public int[] CategoryId { get; set; }
    }

}