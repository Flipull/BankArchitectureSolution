using Architecture.BusinessLogic.BankLogics.Infra;
using Architecture.BusinessLogic.BankSROs;
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

        public IActionResult Index([FromServices] ICustomerLogic customerLogic,
                                    [FromServices] IBankAccountLogic accountLogic,
                                    [FromServices] IBankTransactionLogic transactionLogic)
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


            var acc1 = accountLogic.CreateAccount(updated_customer.Guid);
            var acc2 = accountLogic.CreateAccount(updated_customer.Guid);

            accountLogic.Deposit(
                new BankAccountLiquidizeSRO
                {
                    GuidAccount = acc1.Guid,
                    Value = 10
                });
            accountLogic.Deposit(
                new BankAccountLiquidizeSRO
                {
                    GuidAccount = acc2.Guid,
                    Value = 10
                });
            accountLogic.Withdraw(
                new BankAccountLiquidizeSRO
                {
                    GuidAccount = acc1.Guid,
                    Value = 9
                });
            transactionLogic.Transfer(
                new BankTransactionExecuteSRO
                {
                    GuidAccount = acc2.Guid,
                    IbanAccount = acc2.Iban,
                    IbanTarget = acc1.Iban,
                    Value = 5
                });
            accountLogic.Withdraw(
                new BankAccountLiquidizeSRO
                {
                    GuidAccount = acc1.Guid,
                    Value = 10
                });
            accountLogic.Withdraw(
                new BankAccountLiquidizeSRO
                {
                    GuidAccount = acc1.Guid,
                    Value = 6
                });
            return View(accountLogic.ViewAccounts(updated_customer.Guid));
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
