using ETicaret.Entity.Featur;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETicaret.EntityLayer
{
    public class Categoria: Featurs
    {
        [Key]
        public int CategoriaID { get; set; } 
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}