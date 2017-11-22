using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HamperWeb.Models;
using HamperWeb.Services;
using HamperWeb.ViewModels;


namespace HamperWeb.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Category> _categoryDataService;
        private IDataService<Hamper> _hamperDataService;
        public HomeController(IDataService<Category> service, IDataService<Hamper> hamperService)
        {
            _categoryDataService = service;
            _hamperDataService = hamperService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Hamper> hamperlist = _hamperDataService.Query(h => h.Discontinued == false);
            IEnumerable<Category> categorylist = _categoryDataService.GetAll();
  
            //Create vm
            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Hampers = hamperlist,
                Categories = categorylist
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Index(HomeIndexViewModel vm)
        {
            if(ModelState.IsValid)
            { 
                if (vm.maxPrice == 0)
                {
                    vm.maxPrice = 10000;
                }
                if (vm.CategoryId == 1)
                {
                    IEnumerable<Hamper> query = _hamperDataService.Query(h => h.Discontinued == false);
                    List<Hamper> hamperlist = query.Where(p => p.HamperPrice >= vm.minPrice && p.HamperPrice <= vm.maxPrice).ToList();
                    vm.Hampers = hamperlist;
                }
                else
                {
                    IEnumerable<Hamper> query = _hamperDataService.Query(h => h.CategoryId == vm.CategoryId && h.Discontinued == false);
                    List<Hamper> hamperlist = query.Where(p => p.HamperPrice >= vm.minPrice && p.HamperPrice <= vm.maxPrice).ToList();
                    vm.Hampers = hamperlist;
                }
            
                IEnumerable<Category> categorylist = _categoryDataService.GetAll();
                vm.Categories = categorylist;
            }         
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ToU()
        {
            return View();
        }
    }
}
