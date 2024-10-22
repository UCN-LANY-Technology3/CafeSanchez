using CafeSanchez.POS.Models;
using CafeSanchez.POS.Services.Auth;
using CafeSanchez.POS.Services.OrderManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CafeSanchez.POS.Controllers
{

    [Authorize]
    public class PosController(ILogger<HomeController> logger, OrderManagementService orderService) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly OrderManagementService _orderService = orderService;

        public IActionResult Index()
        {
            // Finding name of the cashier that is signed in
            string cashierName = HttpContext.User.Claims.Single(c => c.Type == "Fullname").Value;

            // Getting list of active orders 
            IEnumerable<OrderModel> orders = _orderService.GetActiveOrdersList();

            // Getting list of available products
            IEnumerable<ProductModel> products = _orderService.GetProductsList();

            // Returning view with data
            return View(new PosModel { CashierName = cashierName, AvailableProducts = products, ActiveOrders = orders });
        }


        [HttpPost("/createorder")]
        public IActionResult CreateOrder(NewOrderModel model)
        {
            return RedirectToAction("Index");
        }
    }
}
