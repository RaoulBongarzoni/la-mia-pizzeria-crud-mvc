using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace la_mia_pizzeria_static.Models
{

    [Table("Pizze")]
    public class Pizza
    {
        [Key]public int Id { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(100, ErrorMessage = "Nome troppo lungo, scegliere un nome più corto") ]
        public string Name { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [StringLength(256, ErrorMessage = "Descrizione troppo lunga")]
        public string Description { get; set; }

        public string?  PhotoURL { get; set; }
        [Required(ErrorMessage = "Il campo è obbligatorio")]
        [Range(1, 20, ErrorMessage = "Il prezzo selezionato deve essere compreso tra 1 e 80, se continui a visualizzare questo errore prova a cambiare il punto con una virgola")]
        public decimal? Price { get; set; }


        //Aggiunta delle categorie

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }


        //aggiunta ingradienti

        public List<Ingredient>? Ingredients { get; set; }




        public Pizza() { 
        }

        public Pizza(string name, string description, string photo, decimal? price)
        {
            this.Name = name;
            this.Description = description;
            this.PhotoURL = photo;

            this.Price = price;
        }
    }
}
