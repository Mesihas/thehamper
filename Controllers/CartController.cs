using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HamperWeb.Models;
using HamperWeb.Services;
using HamperWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HamperWeb.Controllers
{
    public class CartController : Controller
    {
        private IDataService<Hamper> _hamperDataService;
        private IDataService<Order> _orderDataService;
        private IDataService<OrderDetail> _orderDetailDataService;
        private IDataService<Address> _addressDataService;

        public CartController(IDataService<Hamper> hamperService,
                              IDataService<Order> orderService,
                              IDataService<OrderDetail> orderDetailService,
                              IDataService<Address> addressService)
        {

            _hamperDataService = hamperService;
            _orderDataService = orderService;
            _orderDetailDataService = orderDetailService;
            _addressDataService = addressService;
        }
        [Authorize]
        public IActionResult Index(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Order order = _orderDataService.GetSingle(od => od.OrderId == id);
                IEnumerable<OrderDetail> oDList = _orderDetailDataService.Query(od => od.OrderId == id);
                IEnumerable<Hamper> hampersList = _hamperDataService.GetAll();

                var totalItems = oDList.Count();
                var SumItems = oDList.Where(or => or.OrderId == order.OrderId).Sum(p => p.HamperPrice * p.Quantity).ToString();

                CartIndexViewModel vm = new CartIndexViewModel
                {
                    OrderId = id,
                    orderDetail = oDList,
                    Date = order.Date,
                    UserId = order.UserId,
                    OrderState = order.OrderStateId,
                    Hampers = hampersList,
                    totalItems = totalItems,
                    SumItems = SumItems
                };
                return View(vm);
            }
        }
    }
}
