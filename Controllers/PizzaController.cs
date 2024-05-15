using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {

        
        public IActionResult Index()
        {

            var pizzas = dbContext.Pizza 
            return View();
        }
    }
}
