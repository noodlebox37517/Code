using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ViewResult Index()
        {
               var Customers = GetCustomers();

            return View(Customers);
        }
        //GET: Customer details
        public ActionResult Details(int id)
        {
           var customer = GetCustomers().SingleOrDefault(c => c.ID == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
            new Customer {ID = 1, Name = "Albert Abercromby" },
            new Customer {ID = 2, Name = "Bethany Bellatrix" }
            };

        }
    }
}