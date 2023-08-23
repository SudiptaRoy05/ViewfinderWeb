using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Viewfinder.DataAccess.Repository;
using Viewfinder.DataAccess.Repository.IRepository;
using Viewfinder.Models;
using Viewfinder.Models.ViewModels;

namespace ViewfinderWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PhotographController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _he;
        public PhotographController(IUnitOfWork unitOfWork , IWebHostEnvironment he)
        {
            _unitOfWork = unitOfWork;
            _he = he;   
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get 
        public IActionResult Upsert(int? id)
        {
            PhotographVM photographVM = new()
            {
                photograph = new(),

                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

                CompositionList = _unitOfWork.Composition.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })

            };

            if (id == null || id == 0)
            {
                //Upload PhotoGraph
                return View(photographVM);
            }
            else
            {
                photographVM.photograph = _unitOfWork.Photograph.GetFirstOrDefault(u => u.Id == id);
                return View(photographVM);
                //Edit Photograph
            }
            
            
            
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PhotographVM obj, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _he.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();    
                    var uploads = Path.Combine(wwwRootPath, @"Images\Photograph");
                    var extension = Path.GetExtension(file.FileName);

                    if(obj.photograph.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.photograph.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.photograph.ImageUrl = @"\Images\Photograph\" + filename + extension;
                }
                if(obj.photograph.Id == 0)
                {
                    _unitOfWork.Photograph.Add(obj.photograph);
                }
                else
                {
                    _unitOfWork.Photograph.Update(obj.photograph);
                }
                _unitOfWork.Save();
                TempData["success"] = "Photograph Uploaded successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
      /*  //Delete Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Photograph.GetFirstOrDefault(i => i.Id == id);
            if (obj == null) 
            { 
            return NotFound();
            }
            return View(obj);
        }
*/

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var photographList = _unitOfWork.Photograph.GetAll(includeProperties:"Category,Composition");
            return Json(new {data= photographList});
        }

        
        [HttpPost]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Photograph.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deletion" });
            }

            var oldImagePath = Path.Combine(_he.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }


            _unitOfWork.Photograph.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });


        }


        #endregion
    }
}
