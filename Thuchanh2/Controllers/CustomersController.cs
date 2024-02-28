using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Thuchanh2.Models;

namespace Thuchanh2.Controllers
{
    public class CustomersController : Controller
    {
        // GET: CustomersController
        private readonly dbContext _context;

        public CustomersController(dbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var customers = _context.Customers.ToList();
            List<CustomerViewModel> customerList = new List<CustomerViewModel>();
            if (customers != null)
            {
                foreach (var customer in customers)
                {
                    var customerViewModel = new CustomerViewModel()
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Phone= customer.Phone,
                        Email=customer.Email,
                        Mobile=customer.Mobile,
                    };
                    customerList.Add(customerViewModel);
                }
                return View(customerList);
            }
            return View(customerList);
        }

        [HttpGet]
        public IActionResult Search(int? customerId)
        {
            try
            {
                var customers = GetCustomersById(customerId);

                List<CustomerViewModel> customerList = new List<CustomerViewModel>();

                foreach (var customer in customers)
                {
                    var customerViewModel = new CustomerViewModel()
                    {
                        Id = customer.Id,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Phone = customer.Phone,
                        Email = customer.Email,
                        Mobile = customer.Mobile,
                    };
                    customerList.Add(customerViewModel);
                }

                return View("Index", customerList);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        private List<Customer> GetCustomersById(int? customerId)
        {
            var query = _context.Customers.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(o => o.Id == customerId.Value);
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
        public IActionResult Create(Customer customerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = new Customer()
                    {
                        Id = customerViewModel.Id,
                        FirstName = customerViewModel.FirstName,
                        LastName = customerViewModel.LastName,
                        Email = customerViewModel.Email,
                        Mobile = customerViewModel.Mobile,
                        Phone = customerViewModel.Phone,
                    };
                    _context.Customers.Add(customer);
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
                var customer = _context.Customers.SingleOrDefault(x => x.Id == Id);
                if (customer != null)
                {
                    var customers = new Customer()
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Mobile = customer.Mobile,
                        Phone = customer.Phone,
                    };
                    return View(customer);
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
        public IActionResult Edit(Customer model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = new Customer()
                    {
                        Id = model.Id,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Mobile = model.Mobile,
                        Phone = model.Phone,
                    };
                    _context.Customers.Update(customer);
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
                var customer = _context.Customers.SingleOrDefault(x => x.Id == Id);
                if (customer != null)
                {
                    var customers = new Customer()
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Mobile = customer.Mobile,
                        Phone = customer.Phone,
                    };
                    return View(customer);
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
        public IActionResult Delete(Customer model)
        {
            try
            {
                var customer = _context.Customers.SingleOrDefault(x => x.Id == model.Id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
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
