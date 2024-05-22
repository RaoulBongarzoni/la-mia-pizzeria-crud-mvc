using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Pizza> Pizzas { get; set; }


        public Category() { }

        public Category(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }

}
