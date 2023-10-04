using la_mia_pizzeria_static.CustomLoggers;
using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private ICustomLogger _myLogger;
        private PizzaContext _myDatabase;
        public PizzaController(ICustomLogger logger, PizzaContext db)
        {
            _myLogger = logger;
            _myDatabase = db;
        }

        public IActionResult Index()
        {
            _myLogger.WriteLog("L'utente è arrivato sulla pagina Pizza -> Index");
           
            List<Pizza> pizzas = _myDatabase.Pizzas.ToList<Pizza>();
            return View("Admin", pizzas);
            
            
        }

        public IActionResult Details(int id)
        {
            
            Pizza? foundedPizza = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).Include(pizza => pizza.Category).FirstOrDefault();

            if(foundedPizza == null)
            {
                return NotFound($"Nessun risultato trovato con questo id: {id}");
            }
            else
            {
                return View("Details", foundedPizza);
            }
                
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if(!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;

                return View("Create", data);
            }
            
            _myDatabase.Pizzas.Add(data.Pizza);
            _myDatabase.SaveChanges();

            return RedirectToAction("Index");
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDatabase.Categories.ToList();
            PizzaFormModel model = new PizzaFormModel { Pizza = new Pizza(), Categories = categories};

            return View("Create", model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            
            Pizza? pizzaToEdit = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToEdit == null)
            {
                return NotFound("La pizza che vuoi modificare non è stata trovata");
            }
            else
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                PizzaFormModel model = new PizzaFormModel { Pizza = pizzaToEdit, Categories = categories};

                return View("Update", model);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDatabase.Categories.ToList();
                data.Categories = categories;
                return View("Update", data);
            }

           
            Pizza? pizzaToUpdate = _myDatabase.Pizzas.Find(id);

            if (pizzaToUpdate != null)
            {
                EntityEntry<Pizza> entryEntity = _myDatabase.Entry(pizzaToUpdate);
                data.Pizza.Id = id;
                entryEntity.CurrentValues.SetValues(data.Pizza);

                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Mi dispiace, non è stata trovata la pizza da aggiornare");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            
            Pizza? pizzaToDelete = _myDatabase.Pizzas.Where(pizza => pizza.Id == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                _myDatabase.Pizzas.Remove(pizzaToDelete);
                _myDatabase.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("La pizza da eliminare non è stata trovata!");
            }
            
        }


    }
}
