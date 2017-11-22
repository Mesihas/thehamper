﻿using System;
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
using Microsoft.AspNetCore.Http;

namespace HamperWeb.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private RoleManager<IdentityRole> _roleManagerService;
        private IDataService<Profile> _profileDataService;
        public AccountController(UserManager<IdentityUser> managerService, SignInManager<IdentityUser> signService, RoleManager<IdentityRole> roleService, IDataService<Profile> profileService )
        {
            _userManagerService = managerService;
            _signInManagerService = signService;
            _roleManagerService = roleService;
            _profileDataService = profileService;
        }
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [Authorize (Roles="Administrator")]
        public async Task<IActionResult> AddRole(AccountAddRoleViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // create the role
                IdentityRole role = new IdentityRole(vm.Name);
                // add the role to the database
                IdentityResult result = await _roleManagerService.CreateAsync(role);
                if (result.Succeeded)
                {
                    //go to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        //Check  globalization for the language
                    }
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // create the user
                IdentityUser user = new IdentityUser(vm.UserName);
                // add the user to the database
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
            
                if (result.Succeeded)
                {
                    //Add to guest role by default
                    await _userManagerService.AddToRoleAsync(user, "Guest");

                    //Create a empty Role for the user (only user UnserId or UserName)
                    Profile p = new Profile
                    {
                        UserId = vm.UserName
                    };

                    // save to db
                    _profileDataService.Create(p);

                    //go to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                        //Check  globalization for the language
                    }
                }
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl="")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl,
            };

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.Name, vm.Password, vm.RememberMe, false);
        
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "UserName or Password incorrect");
            return View(vm);

        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("totalItems");
            HttpContext.Session.Remove("SumItems");
            HttpContext.Session.Remove("OrderId");
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");     
        }


    }
}
