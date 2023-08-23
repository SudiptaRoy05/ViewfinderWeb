using Microsoft.AspNetCore.Mvc;
using Viewfinder.DataAccess.Repository.IRepository;
using Viewfinder.Models;

namespace ViewfinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompositionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompositionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Composition> composition = _unitOfWork.Composition.GetAll();
            return View(composition);
        }
        //Create Get
        public IActionResult Create()
        {
            return View();
        }

        //Create Post 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Composition obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Composition.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Composition Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //Edit Get
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Composition.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //Edit Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Composition obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Composition.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Composition Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Composition.GetFirstOrDefault(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //Delete Post

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Composition obj)
        {
            if (!ModelState.IsValid)
            {
                _unitOfWork.Composition.Remove(obj);
                _unitOfWork.Save();
                TempData["success"] = "Composition Delete Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
