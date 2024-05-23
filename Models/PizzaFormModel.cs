using Microsoft.AspNetCore.Mvc.Rendering;
using la_mia_pizzeria_static.Data;

namespace la_mia_pizzeria_static.Models
{
    public class PizzaFormModel
    {

        public Pizza Pizza { get; set; }
        public List<Category>? Categories { get; set; }

        public List<SelectListItem>? Ingredients { get; set; }
        public List<string> SelectedIngredients { get; set; }


        public PizzaFormModel() { }

        public PizzaFormModel(Pizza pizza, List<Category> categories)
        {
            this.Pizza = pizza;
            this.Categories = categories;

        }

        public void GenerateIngredients()
        {

            this.Ingredients = new List<SelectListItem>();
            this.SelectedIngredients = new List<string>();

            var ingredientsFromContext = PizzaManager.GetAllIngredients();
            foreach (var item in ingredientsFromContext)
            {
                bool isSelected = this.Pizza.Ingredients?.Any(i => i.Id == item.Id) == true;
                this.Ingredients.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = item.Id.ToString(),
                    Selected = isSelected
                });

                if (isSelected)
                {
                    this.SelectedIngredients.Add(item.Id.ToString());
                }
            }


        }





    }
}
