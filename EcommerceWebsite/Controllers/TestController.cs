using EcommerceWebsiteDbConnection;
using Microsoft.AspNetCore.Mvc;


namespace EcommerceWebsite.Controllers
{
    public class TestController : Controller
    {
        public TestController()
        {
            
        }

        public IActionResult Index(Customer customer)
        {
            return View(customer);
        }
    }
}
