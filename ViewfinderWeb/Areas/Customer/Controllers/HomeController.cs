using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Viewfinder.DataAccess.Repository.IRepository;
using Viewfinder.Models;

namespace ViewfinderWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Photograph> photographs = _unitOfWork.Photograph.GetAll(includeProperties: "Category,Composition");
            return View(photographs);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                Photograph  = _unitOfWork.Photograph.GetFirstOrDefault(u=>u.Id == id,includeProperties: "Category,Composition"),
            };
            
            return View(cartObj);
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