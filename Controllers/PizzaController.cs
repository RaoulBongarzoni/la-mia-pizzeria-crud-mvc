using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;

namespace la_mia_pizzeria_static.Controllers
{
    
    public class PizzaController : Controller
    {

        //Index

        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Index()
        {
            using PizzaContext context = new PizzaContext();


            //lista di tutte le pizze in database
            //List<Pizza> Pizze = context.Pizza.Include(element => element.Category).ToList();

            return View("Index", PizzaManager.GetAllPizzas());
        }

        //Show
        [Authorize(Roles = "ADMIN,USER")]
        public IActionResult Detail(int id) {

            using (PizzaContext context = new PizzaContext())
            {
                Pizza pizzaById = PizzaManager.GetPizzaById(id);
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
        [Authorize(Roles = "ADMIN")]
        //gestore rotta base
        [HttpGet]
        public IActionResult Create()
        {
                PizzaFormModel model = new PizzaFormModel();
                model.Pizza = new Pizza();
                model.Categories = PizzaManager.GetAllCategories();
                model.GenerateIngredients();
                return View(model);
            
        }

        [Authorize(Roles = "ADMIN")]
        //gestore del form di creazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid) {

                
                    data.Categories = PizzaManager.GetAllCategories();
                    data.GenerateIngredients();
                    return View("Create", data);
            }

            PizzaManager.CreatePizza(data.Pizza, data.SelectedIngredients);

            return RedirectToAction("Index");

        }




        //Update
        //gestore rotta per visualizzazione
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id)
        {


                Pizza pizzaToEdit = PizzaManager.GetPizzaById(id);

                if (pizzaToEdit == null)
                    return NotFound();
                else
                {
                    PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetAllCategories());
                    model.GenerateIngredients();

                    return View(model);
                }
           
        }
        //modifica e gestione dei dati ricevuti dal form
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                data.Categories = PizzaManager.GetAllCategories();
                data.GenerateIngredients();
                return View("Update", data);
            }


            //logica sottostante spostata su pizzamanager
            
            if (PizzaManager.UpdatePizza( id, data.Pizza.Name, data.Pizza.Description, data.Pizza.CategoryId, data.SelectedIngredients)
                )
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }


 /*           using (PizzaContext context = new PizzaContext())
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
            } */



        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            bool found = PizzaManager.DeletePizza(id)
            if (found)
            {
                return View("Index");
            }
                else
                {
                    return NotFound();
                }
 
            
        }

    }
}
