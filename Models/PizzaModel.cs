using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    
    public class PizzaModel
    {
        public string PizzaName { get; set; }

        public string PizzaDescription { get; set; }    
        public string PizzaPhoto { get; set; }

        public decimal PizzaPrice {  get; set; }


        public PizzaModel() { }

        public PizzaModel(string name , string description, string photo, decimal price)
        {
            this.PizzaName = name;
            this.PizzaDescription = description;
            this.PizzaPhoto = photo;
            this.PizzaPrice = price;
        }
    }
}
