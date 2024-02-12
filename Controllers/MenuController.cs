using Microsoft.AspNetCore.Mvc;
using Sips.Interfaces;
using Sips.Repositories;
using Sips.SipsModels;
using Sips.Data;

namespace Sips.Controllers
{
    public class MenuController : Controller
    {
        //private readonly IRepository _menuRepo;
        private readonly SipsdatabaseContext _db;



        public MenuController(SipsdatabaseContext db)
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

            var viewModel = new MilkTeaViewModel
            {
                title = "Milk Tea Menu",
                Items = milkTeas.ToList()
            };

            return View(viewModel);
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
