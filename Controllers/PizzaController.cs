using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        //Index
        public IActionResult Index()
        {
            using PizzaContext context = new PizzaContext();


            //lista di tutte le pizze in database
            List<Pizza> Pizze = context.Pizza.Include(element => element.Category).ToList();

            return View("Index", Pizze);
        }

        //Show
        public IActionResult Detail(int id) {

            using (PizzaContext context = new PizzaContext())
            {


                Pizza pizzaById = context.Pizza.Where(element => element.Id == id)
                    .Include(element => element.Category)
                    .Include(element => element.Ingredients).FirstOrDefault();       



                if (pizzaById == null)
                {
                    return NotFound();
                }
                else
                {
                    return View("Detail", pizzaById);
                }

            } ;


        }


        //Create

        //gestore rotta base
        [HttpGet]
        public IActionResult Create()
        {
            using (PizzaContext context = new PizzaContext()) { 
                List<Category> categories = context.Category.ToList();
                List<Ingredient> Ingredients = context.Ingredient.ToList();
                List<SelectListItem> listIngredients = new List<SelectListItem> ();
                PizzaFormModel model = new PizzaFormModel();
                foreach (Ingredient ingredient in Ingredients)
                    {
                        listIngredients.Add(new SelectListItem()
                        {
                            Text = ingredient.Name, Value = ingredient.Id.ToString()

                        });
                    }
                model.Ingredients = listIngredients;
                model.Pizza = new Pizza();
                model.Categories = categories;
                return View( "Create", model);
            }
        }

        //gestore del form di creazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid) {
                using (PizzaContext context = new PizzaContext())
                {
                    List<Category> categories = context.Category.ToList();
                    List<Ingredient> ingredients = context.Ingredient.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();
                    foreach (Ingredient item in ingredients)
                    {
                        listIngredients.Add(
                        new SelectListItem()
                        { Text = item.Name, Value = item.Id.ToString() }
                        );

                    }
                    data.Categories = categories;

                    return View("Create", data);
                }
            }

            using (PizzaContext context = new PizzaContext())
            {

            


                Pizza elementToCreate = new Pizza();
                elementToCreate.Name = data.Pizza.Name;
                elementToCreate.Description = data.Pizza.Description;
                elementToCreate.PhotoURL = data.Pizza.PhotoURL;
                elementToCreate.Price = data.Pizza.Price;

                elementToCreate.CategoryId = data.Pizza.CategoryId;
                List<Ingredient> ingredients = new List<Ingredient>();
                if (data.SelectedIngredients != null)
                {
                    foreach(string slectedIngredientId in data.SelectedIngredients)
                    {
                        
                        int selectedIntIngredientId = int.Parse(slectedIngredientId);
                        Ingredient elementNow = context.Ingredient.Where(i => i.Id == selectedIntIngredientId).FirstOrDefault();
                        ingredients.Add( elementNow );
                    }
                }
                elementToCreate.Ingredients= ingredients;

                context.Pizza.Add(elementToCreate);

                context.SaveChanges();

                return RedirectToAction("Index");
            
            } 

                
            
        }

        //Update
        //gestore rotta per visualizzazione
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzaContext context = new PizzaContext()) {

                Pizza pizzaToEdit = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToEdit == null)
                    return NotFound();
                else
                {
                    List<Category> categories = context.Category.ToList();
                    PizzaFormModel model = new PizzaFormModel();

                    List<Ingredient> Ingredients = context.Ingredient.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();
                    
                    foreach(Ingredient item in Ingredients)
                    {
                        listIngredients.Add(
                            new SelectListItem
                            {
                                Text = item.Name,
                                Value = item.Id.ToString(),
                                //Selected = pizzaToEdit.Ingredients.Any(m => m.Id == item.Id)
                                
                            });
                        ;

                    }
                    
                    
                    model.Ingredients = listIngredients;
                    model.Pizza = pizzaToEdit;
                    model.Categories = categories;

                    return View(model);
                }
            
            
    
            }
        }
        //modifica e gestione dei dati ricevuti dal form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {   
                using (PizzaContext context = new PizzaContext())
                {
                    List<Category> categories = context.Category.ToList();
                    data.Categories = categories;


                    List<Ingredient> ingredients = context.Ingredient.ToList();
                    List<SelectListItem> listIngredients = new List<SelectListItem>();
                    foreach (Ingredient item in ingredients)
                    {
                        listIngredients.Add(
                        new SelectListItem()
                        { Text = item.Name, Value = item.Id.ToString() }
                        );
                    }
                    data.Ingredients = listIngredients;
                    return View("Update", data);
                }
            }

            using (PizzaContext context = new PizzaContext())
            {

                Pizza elementToUpdate = context.Pizza.Where(pizza => pizza.Id == id).Include(e => e.Ingredients).FirstOrDefault();

                elementToUpdate.Ingredients.Clear();
                if (elementToUpdate != null)
                {

                    elementToUpdate.Name = data.Pizza.Name;
                    elementToUpdate.Description = data.Pizza.Description;
                    elementToUpdate.PhotoURL = data.Pizza.PhotoURL;
                    elementToUpdate.Price = data.Pizza.Price;
                    elementToUpdate.CategoryId = data.Pizza.CategoryId;


                    foreach (string selectedIngredientId in data.SelectedIngredients)
                    {
                        int selectedIntIngredientId = int.Parse(selectedIngredientId);
                        Ingredient Ingredient = context.Ingredient
                        .Where(m => m.Id == selectedIntIngredientId)
                        .FirstOrDefault();
                        elementToUpdate.Ingredients.Add(Ingredient);

                    }
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound();
                }
            } 



        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using (PizzaContext context = new PizzaContext()) { 
            
                Pizza pizzaToDelete = context.Pizza.Where(element => element.Id == id).FirstOrDefault();
                if (pizzaToDelete != null)
                {
                    context.Pizza.Remove(pizzaToDelete);
                    context.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    return NotFound();
                }
 
            }
        }

    }
}
