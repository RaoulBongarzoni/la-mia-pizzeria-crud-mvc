using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        
        public IActionResult Index()
        {
            using PizzaContext context = new PizzaContext();



            List<Pizza> Pizze = context.Pizza.ToList();

            return View("Index", Pizze);
        }


        public IActionResult Detail(int Id) {

            using PizzaContext context = new PizzaContext();

            Pizza? pizzaById = new Pizza();        

                pizzaById = (from v in context.Pizza
                        where v.Id == Id
                        select v).FirstOrDefault();

                if (pizzaById == null)
                {
                    return View("Detail", null);
                }



            return View("Detail", pizzaById);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

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
    }
}
