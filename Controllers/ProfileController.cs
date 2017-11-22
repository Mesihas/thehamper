using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HamperWeb.Models;
using HamperWeb.Services;
using HamperWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace HamperWeb.Controllers
{
    public class ProfileController : Controller
    {

        private IDataService<Profile> _profileDataService;
        private IDataService<Address> _addressDataService;
        private UserManager<IdentityUser> _userManagerService;
        //     private IDataService<> _hamperDataService;
        //     private IDataService<Category> _categoryDataService;
        public ProfileController(IDataService<Profile> service, IDataService<Address> service1, UserManager<IdentityUser> managerService)
        {
            _profileDataService = service;
            _addressDataService = service1;
            _userManagerService = managerService;
        }
        
        public IActionResult Index()
        {
            Profile prof = _profileDataService.GetSingle(p => p.UserId == User.Identity.Name);
            IEnumerable<Address> AddressList = _addressDataService.Query(a => a.ProfileId == prof.ProfileId &&  a.Deleted == false);

            //Create vm  
            ProfileIndexViewModel vm = new ProfileIndexViewModel
            {
                Total = AddressList.Count(),
                Address = AddressList,
                FirstName = prof.FirstName,
                LastName = prof.LastName,
                ProfileId = prof.ProfileId
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult UpdateProfile(int id)
        {
            Profile add = _profileDataService.GetSingle(ad => ad.ProfileId == id);

            ProfileUpdateViewService vm = new ProfileUpdateViewService
            {             
                FirstName = add.FirstName,
                LastName =add.LastName,
                ProfileId = add.ProfileId,
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UpdateProfile(ProfileUpdateViewService vm)
        {
            if (ModelState.IsValid)
            {
                Profile p = new Profile
                {                   
                    UserId = User.Identity.Name,
                    ProfileId = vm.ProfileId,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName
                };

                _profileDataService.Update(p);
                return RedirectToAction("Index", "Profile");
            }
            return View(vm);
        }
        [HttpGet]
        [Authorize]
        public IActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateAddress(AddressCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Profile prof = _profileDataService.GetSingle(p => p.UserId == User.Identity.Name);
                // map the Object
                Address a = new Address
                {
                    UserId = User.Identity.Name,
                    ProfileId = prof.ProfileId,
                    City = vm.City,
                    State = vm.State,
                    ZipCode = vm.ZipCode,
                    AddressUser = vm.AddressUser,
                    Default = vm.Default
                };
                // save to db
                _addressDataService.Create(a);
                // Go gome
                return RedirectToAction("Index", "Profile");
            }
            // if not valid
            return View(vm);
        }
        [HttpGet]
        [Authorize]
        public IActionResult UpdateAddress(int id)
        {
            Address add = _addressDataService.GetSingle(ad => ad.AddressId == id);        
            AddressUpdateViewModel vm = new AddressUpdateViewModel
            {
                AddressId = add.AddressId,
                AddressUser = add.AddressUser,
                City = add.City,
                State = add.State,
                ZipCode = add.ZipCode,
                Default = add.Default,
                ProfileId = add.ProfileId,
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize]
        public IActionResult UpdateAddress(AddressUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address a = new Address
                {
                    AddressId = vm.AddressId,
                    UserId = User.Identity.Name,  
                    ProfileId = vm.ProfileId,                
                    City = vm.City,
                    State = vm.State,
                    ZipCode = vm.ZipCode,
                    AddressUser = vm.AddressUser,
                    Default = vm.Default
                };
                _addressDataService.Update(a);
                return RedirectToAction("Index", "Profile");
            }
            return View(vm);
        }
        [Authorize]
        public IActionResult DeleteAddress(int Id)
        {
            Address add = _addressDataService.GetSingle(ad => ad.AddressId == Id);

            add.Deleted = true;
            _addressDataService.Update(add);
            return RedirectToAction("Index", "Profile");
        }

    }
}