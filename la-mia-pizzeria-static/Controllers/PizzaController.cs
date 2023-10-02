using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using(PizzaContext db = new PizzaContext())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();
                return View("Admin", pizzas);
            }
            
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new PizzaContext())
            {
                Pizza? foundedPizza = db.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

                if(foundedPizza == null)
                {
                    return NotFound($"Nessun risultato trovato con questo id: {id}");
                }
                else
                {
                    return View("Details", foundedPizza);
                }
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", pizza);
            }
            
            using(PizzaContext db = new PizzaContext())
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        public IActionResult Create()
        {
            return View("Create");
        }

        public IActionResult Edit()
        {
            return View("Edit");
        }
    }
}
