using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thuchanh2.Models;

namespace Thuchanh2.Controllers
{
    public class ProductOrdersController : Controller
    {
        private readonly dbContext _context;

        public ProductOrdersController(dbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            var pOrders = _context.ProductOrders.Include(o => o.Product).ToList();
            List<ProductOrderViewModel> porderList = new List<ProductOrderViewModel>();
            if (pOrders != null)
            {
                foreach (var porder in pOrders)
                {
                    var productorderViewModel = new ProductOrderViewModel()
                    {
                        Id = porder.Id,
                        Quantity = porder.Quantity,
                        Name = porder.Product.Name,
                        orderId = porder.OrderId,

                    };
                    porderList.Add(productorderViewModel);
                }
                return View(porderList);
            }
            return View(porderList);
        }

        [HttpGet]
        public IActionResult Search(int? Id)
        {
            var pOrders = _context.ProductOrders.Include(o => o.Product).ToList();
            List<ProductOrderViewModel> porderList = new List<ProductOrderViewModel>();

            if (Id.HasValue)
            {
                pOrders = pOrders.Where(o => o.Id == Id.Value).ToList();
            }

            foreach (var porder in pOrders)
            {
                var productorderViewModel = new ProductOrderViewModel()
                {
                    Id = porder.Id,
                    Quantity = porder.Quantity,
                    Name = porder.Product.Name,
                    orderId = porder.OrderId,

                };
                porderList.Add(productorderViewModel);
            }

            return View("Index", porderList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductOrders porderViewModel)
        {
            try
            {

                var porder = new ProductOrders()
                {
                    Id = porderViewModel.Id,
                    Quantity = porderViewModel.Quantity,
                    ProductId = porderViewModel.ProductId,
                    OrderId = porderViewModel.OrderId,
                };
                _context.ProductOrders.Add(porder);
                _context.SaveChanges();
                TempData["successMessage"] = "Order created successfully";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: ProductOrdersController/Edit/5
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var porder = _context.ProductOrders.SingleOrDefault(x => x.Id == Id);
                if (porder != null)
                {
                    var porders = new ProductOrders()
                    {
                        Id = porder.Id,
                        Quantity = porder.Quantity,
                        ProductId = porder.ProductId,
                        OrderId = porder.OrderId,
                    };
                    return View(porder);
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
        public IActionResult Edit(ProductOrders model)
        {
            try
            {

                var porder = new ProductOrders()
                {
                    Id = model.Id,
                    Quantity = model.Quantity,
                    ProductId = model.ProductId,
                    OrderId = model.OrderId,
                };
                _context.ProductOrders.Update(porder);
                _context.SaveChanges();
                TempData["successMessage"] = "Product Order details updated";
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: ProductOrdersController/Delete/5
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var porder = _context.ProductOrders.SingleOrDefault(x => x.Id == Id);
                if (porder != null)
                {
                    var porders = new ProductOrders()
                    {
                        Id = porder.Id,
                        Quantity = porder.Quantity,
                        ProductId = porder.ProductId,
                        OrderId = porder.OrderId,
                    };
                    return View(porder);
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

        // POST: ProductOrdersController/Delete/5
        [HttpPost]
        public IActionResult Delete(ProductOrders model)
        {
            try
            {
                var porder = _context.ProductOrders.SingleOrDefault(x => x.Id == model.Id);
                if (porder != null)
                {
                    _context.ProductOrders.Remove(porder);
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
