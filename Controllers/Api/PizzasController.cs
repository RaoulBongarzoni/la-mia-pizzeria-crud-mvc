using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace la_mia_pizzeria_static.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzasController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPizzasSearch()
        {

            return Ok(PizzaManager.GetAllPizzas());
        }

        [HttpGet("{name}")]
        public IActionResult GetPizzasSearch(string name)
        {

                if (PizzaManager.GetPizzaByName(name) == null)
                {
                    return NotFound();
                }
                return Ok(PizzaManager.GetPizzaByName(name));

        }




        [HttpGet()]
        public IActionResult GetPizzasById(int id)
        {
            var Pizza = PizzaManager.GetPizzaById(id);
            if (Pizza == null)
                return NotFound("Nessun Elemento in database con questo Id");
            return Ok(Pizza);
        }


        [HttpPost]

        public IActionResult CreatePost([FromBody] Pizza data)
        {
            PizzaManager.CreatePizza(data);
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, [FromBody] Pizza data)
        {
            var oldPizza = PizzaManager.GetPizzaById(id);
            if (oldPizza == null)
                return NotFound("ERRORE");
            PizzaManager.UpdatePizza(id, data.Name, data.Description, data.CategoryId, null);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePost(int id)
        {
            bool found = PizzaManager.DeletePizza(id);
            if (found)
                return Ok();
            return NotFound();
        }



    }
}
