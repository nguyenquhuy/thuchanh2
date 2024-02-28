using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Thuchanh2.Models;

namespace Thuchanh2.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        private readonly dbContext _context;

        public ProductsController(dbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            List<ProductViewModel> productList = new List<ProductViewModel>();
            if (products != null)
            {
                foreach (var product in products)
                {
                    var productViewModel = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                    };
                    productList.Add(productViewModel);
                }
                return View(productList);
            }
            return View(productList);
        }

        [HttpGet]
        public IActionResult Search(int? productId)
        {
            try
            {
                var products = GetCustomersById(productId);

                List<ProductViewModel> productList = new List<ProductViewModel>();

                foreach (var product in products)
                {
                    var productViewModel = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                    };
                    productList.Add(productViewModel);
                }

                return View("Index", productList);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        private List<Products> GetCustomersById(int? productId)
        {
            var query = _context.Products.AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(o => o.Id == productId.Value);
            }
            return query.ToList();
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        public IActionResult Create(Products productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Products()
                    {
                        Id = productViewModel.Id,
                        Name = productViewModel.Name,
                        Price = productViewModel.Price,
                        
                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    TempData["successMessage"] = "Customer created successfully";
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

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(x => x.Id == Id);
                if (product != null)
                {
                    var products = new Products()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        
                    };
                    return View(product);
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
        public IActionResult Edit(Products model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = new Products()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Price = model.Price,
                    };
                    _context.Products.Update(product);
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

        // GET: CustomersController/Delete/5
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(x => x.Id == Id);
                if (product != null)
                {
                    var products = new Products()
                    {
                        Name = product.Name,
                        Price = product.Price,
                        
                    };
                    return View(product);
                }
                else
                {
                    TempData["errorMessage"] = $"Product details not available with Id: {Id}";
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
        public IActionResult Delete(Products model)
        {
            try
            {
                var product = _context.Products.SingleOrDefault(x => x.Id == model.Id);
                if (product != null)
                {
                    _context.Products.Remove(product);
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
