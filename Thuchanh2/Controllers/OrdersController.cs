using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thuchanh2.Models;

namespace Thuchanh2.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderDbContext _context;

        public OrdersController(OrderDbContext context)
        {
            this._context = context;
        }

        //Get data
        [HttpGet]
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();
            List<OrderViewModel> orderList = new List<OrderViewModel>();
            if (orders != null)
            {                
                foreach (var order in orders) {
                    var orderViewModel = new OrderViewModel()
                    {
                        Id = order.Id,
                        OrderDate = order.OrderDate,
                        FirstName = order.Customer.FirstName,
                        LastName = order.Customer.LastName,
                    };
                    orderList.Add(orderViewModel);
                }
                return View(orderList);
            }
            return View(orderList);
        }

        //Search
        [HttpGet]
        public IActionResult Search(int? customerId)
        {
            try
            {
                var orders = GetOrdersByCustomerId(customerId);

                List<OrderViewModel> orderList = new List<OrderViewModel>();

                foreach (var order in orders)
                {
                    var orderViewModel = new OrderViewModel()
                    {
                        Id = order.Id,
                        OrderDate = order.OrderDate,
                        FirstName = order.Customer.FirstName,
                        LastName = order.Customer.LastName,
                    };
                    orderList.Add(orderViewModel);
                }

                return View("Index", orderList);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        private List<Orders> GetOrdersByCustomerId(int? customerId)
        {
            var query = _context.Orders.Include(o => o.Customer).AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == customerId.Value);
            }

            return query.ToList();
        }



        //Create
        [HttpGet]
        public IActionResult Create() {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Orders orderViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = new Orders()
                    {
                        Id = orderViewModel.Id,
                        OrderDate = orderViewModel.OrderDate,
                        CustomerId = orderViewModel.CustomerId
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Order created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int Id) {
            try
            {
                var order = _context.Orders.SingleOrDefault(x => x.Id == Id);
                if (order != null)
                {
                    var orders = new Orders()
                    {
                        OrderDate = order.OrderDate,
                        CustomerId = order.CustomerId
                    };
                    return View(order);
                }
                else
                {
                    TempData["errorMessage"] = $"Order details not available with Id: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Edit(Orders model) {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = new Orders()
                    {
                        Id = model.Id,
                        OrderDate = model.OrderDate,
                        CustomerId = model.CustomerId
                    };
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Order details updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        //Delete
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(x => x.Id == Id);
                if (order != null)
                {
                    var orders = new Orders()
                    {
                        OrderDate = order.OrderDate,
                        CustomerId = order.CustomerId
                    };
                    return View(order);
                }
                else
                {
                    TempData["errorMessage"] = $"Order details not available with Id: {Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }

        }

        [HttpPost]
        public IActionResult Delete(Orders model)
        {
            try
            {
                var order = _context.Orders.SingleOrDefault(x => x.Id == model.Id);
                if (order != null)
                {
                    _context.Orders.Remove(order);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Order deleted";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Order details not available with Id: {model.Id}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
