using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HamperWeb.Models;
using HamperWeb.Services;
using HamperWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Driver;

namespace HamperWeb.Controllers
{
    public class OrderController : Controller
    {
        private IDataService<Order> _orderDataService;
        private IDataService<OrderDetail> _orderDetailDataService;
      
        public OrderController(IDataService<Order> service, IDataService<OrderDetail> service2)
        {
            _orderDataService = service;
            _orderDetailDataService = service2;
        }
        [Authorize]
        public IActionResult Add(HamperDetailViewModel vm)
        {
            if (HttpContext.Session.GetInt32("OrderId") == null)
            {
                Order o = new Order
                {
                    UserId = User.Identity.Name,
                    Date = System.DateTime.Now,
                    OrderStateId = 1 //placed or started
                };
                _orderDataService.Create(o);
                Int32 orderNumber = o.OrderId;
                HttpContext.Session.SetInt32("OrderId", orderNumber);

                ///////////  MongoDb Order Header ///////////////////////////////////////////////////////////////////
                //MongoDBContext dbContextOrderHeader = new MongoDBContext();
                //OrderMongo ordM = new OrderMongo
                //{
                //    UserId = User.Identity.Name,
                //    Date = System.DateTime.Now,
                //    OrderStateId = 1 //placed or started
                //};
                //ordM.OrderId = Guid.NewGuid();

                //dbContextOrderHeader.OrdersMongo.InsertOne(ordM);
                //Guid tempMongoOrderId = ordM.OrderId;
                //HttpContext.Session.SetString("OrderIdM", tempMongoOrderId.ToString());

                //////////////////


                //MongoDBContext dbContextOrderHeader = new MongoDBContext();

                //OrderCompleteMongo ocm =new OrderCompleteMongo
                //{
                //    UserId = User.Identity.Name,
                //    Date = System.DateTime.Now,
                //    AddressId = 0,
                //    OrderStateId = 1,
                //    OrderDetails = {
                //        {HamperId = 123 },
                //        {Quantity = 123},
                //        {HamperPrice = 123} }
                // };
        /////////// END  MongoDb /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
            else
            {
                HttpContext.Session.GetInt32("OrderId");
            }   
                      
            OrderDetail od = new OrderDetail
            {
                OrderId = Convert.ToInt32(HttpContext.Session.GetInt32("OrderId")),
                Quantity = vm.Quantity,
                HamperId = vm.HamperId,
                HamperPrice = vm.HamperPrice
            };
            _orderDetailDataService.Create(od);

            ///////////  MongoDb  Order Detail    /////////////////////////////////////////////////////////////////////////
            //MongoDBContext dbContextOrderdetail = new MongoDBContext();
            //OrderDetailMongo ordDetM = new OrderDetailMongo
            //{
            //    OrderId = HttpContext.Session.GetString("OrderIdM"),
            //    HamperId = vm.HamperId,
            //    Quantity = vm.Quantity,
            //    HamperPrice = vm.HamperPrice
            //};

            //ordDetM.OrderDetailId = Guid.NewGuid();
            //dbContextOrderdetail.OrdersDetailMongo.InsertOne(ordDetM);

            /////////// END  MongoDb ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            IEnumerable<OrderDetail> ord = _orderDetailDataService.Query(or => or.OrderId == od.OrderId);
            var totalItems = ord.Count();
            var SumItems = ord.Where(or => or.OrderId == od.OrderId).Sum(p => p.HamperPrice * p.Quantity).ToString();
            HttpContext.Session.SetInt32("totalItems", totalItems);
            HttpContext.Session.SetString("SumItems", SumItems);

            return RedirectToAction("Index", "Cart", new {id = od.OrderId });
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(OrderCreateViewModel vm)
        {         
            if (ModelState.IsValid)
            {
                //Map to object Order
                Order o = new Order
                {
                   
                };
                // save to db
                _orderDataService.Create(o);
                // Go gome
                return RedirectToAction("Index", "Order");
            }
            // if not valid
            return View(vm);
        }
        [Authorize]
        public IActionResult Detail(int orderId)
        {
            //Create vm
            Order order = _orderDataService.GetSingle(o => o.OrderId == orderId);
            return View();
        }
    }
}
