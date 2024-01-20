using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace BookStore.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //IEnumerable<Project> productList = -_unitOfWork.Products.Getall(includeProperties: "Category,CoverType");
            return View();
        }

        //public IActionResult Details()
        //{
        //    ProductHeaderValue product = _unitOfWork.Product.GetfirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType");
        //    return View(product);
        //}

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
