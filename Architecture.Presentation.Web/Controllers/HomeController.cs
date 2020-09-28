using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.Presentation.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Architecture.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerLogic _customerLogic;
        public HomeController(ICustomerLogic customerlogic, ILogger<HomeController> logger)
        {
            _customerLogic = customerlogic;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var new_customer = _customerLogic.CreateCustomer(
                    new CustomerDTO { FirstName = "AAA", Initials = "B", LastName = "CCCCCC" });
            new_customer.LastName = "Di Daggio";
            new_customer.FirstName = "Marco";
            var updated_customer = _customerLogic.UpdateCustomer(new_customer);
            return View(_customerLogic.ViewCustomer(new_customer.Guid));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
