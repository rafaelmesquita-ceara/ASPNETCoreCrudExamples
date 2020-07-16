using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookListMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CH_Name { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CH_Login { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public string CH_Password { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public int VL_Age { get; set; }

    }
}
