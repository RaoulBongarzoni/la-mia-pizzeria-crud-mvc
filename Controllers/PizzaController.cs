using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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


                Pizza pizzaById = context.Pizza.Where(element => element.Id == id).Include(element => element.Category).FirstOrDefault();       



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
            PizzaFormModel model = new PizzaFormModel();
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

                    return View("Create", data);
                }
            }

            using (PizzaContext context = new PizzaContext())
            {

                Pizza elementToUpdate = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (elementToUpdate != null)
                {

                    elementToUpdate.Name = data.Pizza.Name;
                    elementToUpdate.Description = data.Pizza.Description;
                    elementToUpdate.PhotoURL = data.Pizza.PhotoURL;
                    elementToUpdate.Price = data.Pizza.Price;
                    elementToUpdate.CategoryId = data.Pizza.CategoryId;


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
