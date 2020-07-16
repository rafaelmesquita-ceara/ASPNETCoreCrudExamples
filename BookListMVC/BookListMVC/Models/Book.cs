using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

    }
}