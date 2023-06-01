using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;

namespace SellerProduct.Controllers
{


    public class RegisterController : Controller
    {
        private readonly IUserServices _userServices; // Interface
        public RegisterController(ILogger<RegisterController> logger)
        {
            _userServices = new UserServices();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        public IActionResult DangKy(User user)
        {

            _userServices.CreateUser(user);
            return View("Index");
        }


        [HttpGet]
        public IActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public IActionResult DangNhap(User user)
        {

            return View();
        }
    }
}
