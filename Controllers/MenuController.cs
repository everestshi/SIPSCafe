using Microsoft.AspNetCore.Mvc;
using Sips.Interfaces;
using Sips.Repositories;
using Sips.Models;
using Sips.Data;

namespace Sips.Controllers
{
    public class MenuController : Controller
    {
        //private readonly IRepository _menuRepo;
        private readonly SipsContext _db;



        public MenuController(SipsContext db)
        {
            _db = db;
        }

        public IActionResult MenuIndex()
        {
            return View();
        }

        public IActionResult MilkTea()
        {
            MenuRepo menuRepo = new MenuRepo(_db);

            var milkTeas = menuRepo.GetMilkTeas();
            return View(milkTeas);
        }

        public IActionResult FruitTea()
        {
            MenuRepo menuRepo = new MenuRepo(_db);

            var fruitTeas = menuRepo.GetFruitTeas();
            return View(fruitTeas);
        }

        public IActionResult Slush()
        {
            MenuRepo menuRepo = new MenuRepo(_db);

            var slushes = menuRepo.GetSlushes();
            return View(slushes);
        }
    }
}
