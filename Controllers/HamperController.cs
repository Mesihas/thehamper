using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HamperWeb.Models;
using HamperWeb.Services;
using HamperWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//...these namespaces for uploading picture and creating subfolder with the username 
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HamperWeb.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Category> _categoryDataService;
        private IHostingEnvironment _environment;
        public HamperController(IDataService<Hamper> service, 
            IDataService<Category> service1,
            IHostingEnvironment environment)
        {
            _hamperDataService = service;
            _categoryDataService = service1;
            _environment = environment;
        }

        public IActionResult Index()
        {
            IEnumerable<Hamper> list = _hamperDataService.GetAll();
            IEnumerable<Category> catList = _categoryDataService.GetAll();
            //Create vm
            HamperIndexViewModel vm = new HamperIndexViewModel
            {
                Hampers = list,
                Total = list.Count(),
                Categories = catList
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            IEnumerable<Category> catList = _categoryDataService.GetAll();

            HamperCreateViewModel vm = new HamperCreateViewModel
            {
                Categories = catList,
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create(HamperCreateViewModel vm, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                // Create new Hamper
                Hamper h = new Hamper
                {                
                     HamperName = vm.HamperName,
                     HamperPrice = vm.HamperPrice,
                     Description = vm.Description,
                     CategoryId = vm.CategoryId,
                     Discontinued = false,
                };
                //if file is to be uploaded

                if (ImageUrl != null)
                {
                    //upload server path
                    var uploadPath = Path.Combine(_environment.WebRootPath, "ima/uploads");
                    //create folder with username
                    Directory.CreateDirectory(Path.Combine(uploadPath, User.Identity.Name));
                    //work with the file name - try to use helper class
               //     string extension = Path.GetExtension(file.FileName);
                    string fileName = FileNameHelper.GetNameFormat(ImageUrl.FileName);
                    //copy file from client to server
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(fileStream);
                    }
                    //add path to category
                    h.ImageUrl = "/ima/uploads" + "/" + User.Identity.Name + "/" + fileName;
                }//end file is to be uploaded

                // save to db
                _hamperDataService.Create(h);
                // Go gome
                return RedirectToAction("Index", "Hamper");
            }
            // if not valid
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult Update(int id)
        {

            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == id);
            IEnumerable<Category> catList = _categoryDataService.GetAll();

            HamperUpdateViewModel vm = new HamperUpdateViewModel
            {
                HamperId = id,
                HamperName = hamper.HamperName,
                HamperPrice = hamper.HamperPrice,
                Description = hamper.Description,
                CategoryId = hamper.CategoryId,
                Discontinued = hamper.Discontinued,
                Categories = catList,
                ImageUrl = hamper.ImageUrl
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Update(HamperUpdateViewModel vm, IFormFile ImageUrl)
        {
            if (ModelState.IsValid)
            {
                Hamper h = new Hamper
                {
                    HamperId = vm.HamperId,
                    HamperName = vm.HamperName,
                    HamperPrice = vm.HamperPrice,
                    Description = vm.Description,
                    CategoryId = vm.CategoryId,
                    Discontinued = vm.Discontinued,
                };
                //if file is to be uploaded
                if (ImageUrl != null)
                {
                    //upload server path
                    var uploadPath = Path.Combine(_environment.WebRootPath, "ima/uploads");
                    //create folder with username
                    Directory.CreateDirectory(Path.Combine(uploadPath, User.Identity.Name));
                    //work with the file name - try to use helper class
                    //     string extension = Path.GetExtension(file.FileName);
                    string fileName = FileNameHelper.GetNameFormat(ImageUrl.FileName);
                    //copy file from client to server
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, User.Identity.Name, fileName), FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(fileStream);
                    }
                    //add path to category
                    h.ImageUrl = "/ima/uploads" + "/" + User.Identity.Name + "/" + fileName;
                }//end file is to be uploaded

                // save to db
                _hamperDataService.Update(h);
                return RedirectToAction("Index", "Hamper");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            Hamper hamper = _hamperDataService.GetSingle(h => h.HamperId == Id);          
            //Create vm
            HamperDetailViewModel vm = new HamperDetailViewModel
            {
                HamperId = hamper.HamperId,
                HamperName = hamper.HamperName,
                HamperPrice = hamper.HamperPrice,
                Description = hamper.Description,
                ImageUrl = hamper.ImageUrl,
                //totalItems = Convert.ToInt32(HttpContext.Session.GetInt32("totalItems")),
                //SumItems = HttpContext.Session.GetString("SumItems")
            };
            return View(vm);
        }
    }
}

