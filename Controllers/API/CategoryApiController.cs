using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HamperWeb.Services;
using HamperWeb.Models;
using HamperWeb.ViewModels;
using System.Net;

namespace HamperWeb.Controllers.API
{
  //  [Route("api/[controller]")]
    public class CategoryApiController : Controller
    {
        private IDataService<Category> _categoryDataSerivce;
        private IDataService<Hamper> _hamperDataService;

        public CategoryApiController(IDataService<Category> service, IDataService<Hamper> service2
)        {
            _categoryDataSerivce = service;
            _hamperDataService = service2;
        }

        //Web Methd to get all categories
        [HttpGet("api/categories")]
        public JsonResult GetCategories()
        {
            try
            {
                IEnumerable<Category> cats = _categoryDataSerivce.GetAll();
                return Json(cats);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }        
        }

        [HttpPost("api/categories/Create")]
        public JsonResult PostCategory([FromBody] CategoryCreateViewModel vm)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //Map to model
                    Category cat = new Category
                    {
                        Name = vm.Name,
                        Details = vm.Details
                    };
                    _categoryDataSerivce.Create(cat);
                    return Json(cat);
                }
                //invalid
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "fail", ModelState = ModelState });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }       
        }

        /// get prducts by category name
        [HttpGet("api/products")]
        public JsonResult GetProductsByCategory(string catName)
        {
            try
            {
                Category cat = _categoryDataSerivce.GetSingle(c => c.Name == catName);
                if (cat !=null)
                {
                    if(catName== "All Categories")
                    {
                        IEnumerable<Hamper> products = _hamperDataService.Query(h => h.Discontinued == false);
                          return Json(products);
                    }
                    else
                    {
                        IEnumerable<Hamper> products = _hamperDataService.Query(p => p.CategoryId == cat.CategoryId && p.Discontinued == false);
                        return Json(products);
                    }           
                }
                else
                {
                    return Json(new { message = "can not find this category" });
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = ex.Message });
            }
        }
    }
}
