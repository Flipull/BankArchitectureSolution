using Architecture.BusinessLogic.CustomerDTOs;
using Architecture.BusinessLogic.CustomerLogics.Infra;
using Architecture.BusinessLogic.CustomerSROs;
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
                    new CustomerCreateSRO { FirstName = "AAA", Initials = "B", LastName = "CCCCCC" });

            var update_customer =
                    new CustomerUpdateSRO
                    {
                        Guid = new_customer.Guid,
                        FirstName = "David",
                        Initials = "J",
                        LastName = "Solomons"
                    };
            var updated_customer = customerLogic.UpdateCustomer(update_customer);

            return View(customerLogic.ViewCustomer(updated_customer.Guid));
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
