using Microsoft.AspNetCore.Mvc;
using Sips.Interfaces;
using Sips.Repositories;
using Sips.SipsModels;
using Sips.ViewModels;
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

            var viewModel = new MenuItemVM
            {
                Title = "Milk Tea Menu",
                Items = milkTeas.ToList(),
                //IceOptions = menuRepo.GetIceOptions(),
                //SweetnessOptions = menuRepo.GetSweetnessOptions(),
                //MilkOptions = menuRepo.GetMilkOptions(),
                //AddonOptions = menuRepo.GetAddonOptions()
            };

            return View(viewModel);
        }


        public IActionResult FruitTea1()
        {
            MenuRepo menuRepo = new MenuRepo(_db);

            var fruitTeas = menuRepo.GetFruitTeas();

            var viewModel = new MenuItemVM
            {
                Title = "Fruit Tea Menu",
                Items = fruitTeas.ToList(),
                //IceOptions = menuRepo.GetIceOptions(),
                //SweetnessOptions = menuRepo.GetSweetnessOptions(),
                //MilkOptions = menuRepo.GetMilkOptions(),
                //AddonOptions = menuRepo.GetAddonOptions()
            };
            return View(viewModel);
        }

        public IActionResult Slush()
        {
            MenuRepo menuRepo = new MenuRepo(_db);

            var slushes = menuRepo.GetSlushes();

            var viewModel = new MenuItemVM
            {
                Title = "Slushies Menu",
                Items = slushes.ToList(),
                //IceOptions = menuRepo.GetIceOptions(),
                //SweetnessOptions = menuRepo.GetSweetnessOptions(),
                //MilkOptions = menuRepo.GetMilkOptions(),
                //AddonOptions = menuRepo.GetAddonOptions()
            };

            return View(viewModel);
        }
    }
}
