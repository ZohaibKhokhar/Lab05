using Lab05.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab05.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            UserRepository repo=new UserRepository();
            repo.AddUser(user);
            TempData["Message"] = "Login Successful";
            return RedirectToAction("Index");
        }

        public IActionResult DisplayUsers()
        {
            UserRepository repo = new UserRepository();
            List<User> users = repo.GetAll();
            return View(users);
        }
  
        public IActionResult SetCookie()
        {
            CookieOptions option = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(10)
            };
            Response.Cookies.Append("UserName","Zohaib Khokhar",option);
            return Content("cookie set Succesfully!");
            
        }

        public IActionResult GetCookie()
        {
            string username = Request.Cookies["UserName"];
            if (username != null)
            {
                return Content(username);
            }
            else 
            {
                return Content("Cookie not found");
            }
        }

        public IActionResult DeleteCookie()
        {
            Response.Cookies.Delete("UserName");
            return Content("Cookie deleted successfully.");
        }

      
        public ActionResult AddToCart(int id)
        {
            TempData["Message"] = $"Product with id={id} has been added to cart";
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
