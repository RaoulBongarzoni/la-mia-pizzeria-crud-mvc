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
            List<Pizza> Pizze = context.Pizza.ToList();

            return View("Index", Pizze);
        }

        //Show
        public IActionResult Detail(int id) {

            using PizzaContext context = new PizzaContext();

            Pizza pizzaById = context.Pizza.Where(element => element.Id == id).FirstOrDefault();       



            if (pizzaById == null)
            {
                return NotFound();
            }
            else
            {
                return View("Detail", pizzaById);
            }



        }


        //Create

        //gestore rotta base
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //gestore del form di creazione
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            PizzaContext context = new PizzaContext();
            Pizza elementToCreate = new Pizza();
            elementToCreate.Name = data.Name;
            elementToCreate.Description = data.Description;
            elementToCreate.PhotoURL = data.PhotoURL;
            elementToCreate.Price = data.Price;

            context.Pizza.Add(elementToCreate);

            context.SaveChanges();

            return RedirectToAction("Index");

                
            
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
                    return View(pizzaToEdit);
            
            
    
            }
        }
        //modifica e gestione dei dati ricevuti dal form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza data)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", data);
            }

            PizzaContext context = new PizzaContext();
            Pizza elementToUpdate = context.Pizza.Where(pizza => pizza.Id == id).FirstOrDefault();
            if (elementToUpdate != null)
            {

            elementToUpdate.Name = data.Name;
            elementToUpdate.Description = data.Description;
            elementToUpdate.PhotoURL = data.PhotoURL;
            elementToUpdate.Price = data.Price;


            context.SaveChanges();
            return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
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
