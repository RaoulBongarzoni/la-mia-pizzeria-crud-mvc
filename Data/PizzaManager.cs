using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace la_mia_pizzeria_static.Data
{
    public class PizzaManager
    {
        public static List<Pizza> GetAllPizzas()
        {
            using PizzaContext context = new PizzaContext();
            return context.Pizza.ToList();
        }

        public static List<Category> GetAllCategories()
        {
            using PizzaContext context = new PizzaContext();
            return context.Category.ToList();
        }

        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext context = new PizzaContext();
            return context.Ingredient.ToList();
        }

        public static Pizza GetPizzaById(int id, bool includeInfo = true)
        {
            using PizzaContext context = new PizzaContext();
            if (includeInfo == true)
            {
                return context.Pizza.Where(element => element.Id == id)
                   .Include(element => element.Category)
                   .Include(element => element.Ingredients).FirstOrDefault();
            }
            return context.Pizza.Where(element => element.Id == id).FirstOrDefault();
            

            
        }
        public static List<Pizza> GetPizzaByName(string name, bool includeInfo = true)
        {
            using PizzaContext context = new PizzaContext();
            if (includeInfo == true)
            {
                return context.Pizza.Where(element => element.Name.Contains(name))
                   .Include(element => element.Category)
                   .Include(element => element.Ingredients).ToList();
            }

                return context.Pizza.Where(element => element.Name.Contains(name)).ToList(); 
           



        }


        public static void CreatePizza(Pizza pizza, List<string> SelectedIngredients = null)
        {

            using PizzaContext context = new PizzaContext();
            if (SelectedIngredients != null)
            {
                pizza.Ingredients = new List<Ingredient>();
                foreach (var ingredientId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = context.Ingredient.FirstOrDefault(i => i.Id == id);
                    pizza.Ingredients.Add(ingredient);

                }
            }
            context.Pizza.Add(pizza);
            context.SaveChanges();
        }



        public static bool UpdatePizza(int id , string name,  string description, int? categoryId, List<string> selectedingredients) {

            using PizzaContext context = new PizzaContext();
            var pizzaToUpdate = context.Pizza.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaToUpdate == null)
            {
                return false;

            }

            pizzaToUpdate.Name = name;
            pizzaToUpdate.Description = description;
            pizzaToUpdate.CategoryId = categoryId;

            pizzaToUpdate.Ingredients.Clear();


            if( selectedingredients != null )
            {
                foreach (var item in  selectedingredients)
                {
                    int ingredientId = int.Parse(item);
                    var ingredientFromContext = context.Ingredient.FirstOrDefault(x => x.Id == ingredientId);
                    pizzaToUpdate.Ingredients.Add(ingredientFromContext);

                }

            }
            context.SaveChanges();

            return true;
        }

        public static bool DeletePizza(int id)
        {
            using PizzaContext context = new PizzaContext();
            var pizzaTodelete = context.Pizza.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaTodelete == null)
            {
                return false;

            }
            context.Remove(pizzaTodelete);
            context.SaveChanges();
            return true;

        }

        



        
    }
}


//da implementare: middle layer