
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models
{

    [Table("Ingredient")]
    public class Ingredient
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza>? Pizzas { get; set; }

        public Ingredient() { }

    }
}
