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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index([FromServices] ICustomerLogic customerLogic)
        {
            var new_customer = customerLogic.CreateCustomer(
                    new CustomerDTO { FirstName = "AAA", Initials = "B", LastName = "CCCCCC" });
            new_customer.LastName = "Di Daggio";
            new_customer.FirstName = "Marco";
            var updated_customer = customerLogic.UpdateCustomer(new_customer);
            return View(customerLogic.ViewCustomer(new_customer.Guid));
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
