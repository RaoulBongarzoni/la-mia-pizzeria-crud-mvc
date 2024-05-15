using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{

    [Table("Pizze")]
    public class Pizza
    {
        [Key]public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public string PhotoURL { get; set; }

        public decimal Price { get; set; }


        public Pizza() { }

        public Pizza(string name, string description, string photo, decimal price)
        {
            this.Name = name;
            this.Description = description;
            this.PhotoURL = photo;
            this.Price = price;
        }
    }
}
