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
    public class CheckoutController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Order> _orderDataService;
        private IDataService<OrderDetail> _orderDetailDataService;
        private IDataService<Address> _addressDataService;
        private IDataService<Profile> _profileDataService;

        public CheckoutController(IDataService<Hamper> hamperService,
                      IDataService<Order> orderService,
                      IDataService<OrderDetail> orderDetailService,
                      IDataService<Address> addressService,
                      IDataService<Profile> profileService)
        {
            _hamperDataService = hamperService;
            _orderDataService = orderService;
            _orderDetailDataService = orderDetailService;
            _addressDataService = addressService;
            _profileDataService = profileService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index(int id)
        {
            /////// BAD practice To use Profile //////////
            //   Profile prof = _profileDataService.GetSingle(p => p.UserId == User.Identity.Name);
            //    IEnumerable<Address> AddressList = _addressDataService.Query(a => a.ProfileId == prof.ProfileId && a.Deleted == false);
            /////////////////////////////////////////////

            IEnumerable<Address> AddressList = _addressDataService.Query(a => a.UserId == User.Identity.Name && a.Deleted == false);
            Order order = _orderDataService.GetSingle(od => od.OrderId == id);
            IEnumerable<OrderDetail> oDList = _orderDetailDataService.Query(od => od.OrderId == id);          

            var totalItems = oDList.Count();
            var SumItems = oDList.Where(or => or.OrderId == order.OrderId).Sum(p => p.HamperPrice * p.Quantity).ToString();

            CheckoutIndexViewModel vm = new CheckoutIndexViewModel
            {
                OrderId = id,
                //     orderDetail = oDList,
                Date = order.Date,
                UserId = order.UserId,
                OrderState = order.OrderStateId,
                totalItems = totalItems,
                SumItems = SumItems,
                AddressList = AddressList,
                TotalAddress = AddressList.Count()
            };
            return View(vm);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Index(CheckoutIndexViewModel vm)
        {
            var x = vm.DefaultAddress;
            return RedirectToAction("Index", "Home");
        }
    }
}

