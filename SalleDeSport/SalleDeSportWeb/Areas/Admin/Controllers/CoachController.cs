using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Models.ViewModels;
using SalleDeSport.Utility;

namespace SalleDeSportWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CoachController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public CoachController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _UnitOfWork = UnitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int? Id)
        {
            CoachVM CoachVM = new()
            {
                Coach = new(),
                ListDesActivites = _UnitOfWork.Activite.GetAll().Select(i => new SelectListItem { 
                    Text=i.Nom,
                    Value=i.Id.ToString()
                })
            };
            if (Id == null || Id == 0)
            {
                //Create brand
                return View(CoachVM);
            }
            else
            {
                //Update product
                CoachVM.Coach = _UnitOfWork.Coach.GetFirstOrDefault(u => u.Id == Id);
                return View(CoachVM);
            }
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoachVM obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, @"images\coachs");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.Coach.ImgUrl != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Coach.ImgUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    //copy the file uploaded inside the product folder
                    using (var fileStreams = new FileStream(Path.Combine(uploads, FileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    //what we will save in the DB
                    obj.Coach.ImgUrl = @"\images\coachs\" + FileName + extension;
                }
                if (obj.Coach.Id == 0)
                {
                    _UnitOfWork.Coach.Add(obj.Coach);
                    TempData["success"] = "coach ajouté avec succès";

                }
                else
                {
                    _UnitOfWork.Coach.Update(obj.Coach);
                    TempData["success"] = "coach modifié avec succès";
                }
                _UnitOfWork.Save();               
               return RedirectToAction("Index");
            }
            return View(obj);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var brandtList = _UnitOfWork.Coach.GetAll();
            return Json(new { data = brandtList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Coach.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _UnitOfWork.Coach.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Coach supprimé avec succès" });

        }
        #endregion
    }
}

