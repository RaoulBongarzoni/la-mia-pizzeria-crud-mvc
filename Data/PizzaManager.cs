using la_mia_pizzeria_static.Models;
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
        



        
    }
}


//da implementare: middle layer