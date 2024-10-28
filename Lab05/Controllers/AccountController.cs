using Lab05.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab05.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = "Succesful login";
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email,string password)
        {
            UserRepository userRepository = new UserRepository();
            List<User> users =  new List<User>();
            users=userRepository.GetAll();
            string name=" ";
            bool found=false;
            foreach (var user in users)
            {
                if (user.Email == email && user.Password == password)
                {
                    name = user.Name;
                    found = true;
                }
            }
            if (found)
            {
                ViewBag.Message = "Successful Login to the website";
                CookieOptions option = new CookieOptions();
                Response.Cookies.Append("UserName",name, option);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Register");
            }
          
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Message = "Email does not exist";
            return View();
        }
        [HttpPost]
        public IActionResult Register(string name,string email,string password)
        {
            UserRepository userRepository = new UserRepository();
            User user=new User() {
                Name= name,
                Email= email,
                Password= password,
                CreatedDate= DateTime.Now
            };
            userRepository.AddUser(user);
            return RedirectToAction("Login");
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("UserName");
            return Content("Cookie deleted successfully.");
        }
    }
}
