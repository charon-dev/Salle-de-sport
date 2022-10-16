using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Utility;

namespace SalleDeSportWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ActiviteController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ActiviteController(IUnitOfWork UnitOfWork, IWebHostEnvironment hostEnvironment)
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
            Activite activite = new();
            if (Id == null || Id == 0)
            {
                //Create brand
                return View(activite);
            }
            else
            {
                //Update product
                activite = _UnitOfWork.Activite.GetFirstOrDefault(u => u.Id == Id);
                return View(activite);
            }
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Activite obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    //Generate new file name
                    string FileName = Guid.NewGuid().ToString();
                    //find the location where the files should be uploaded
                    var uploads = Path.Combine(wwwRootPath, @"images\activites");
                    //keep same extension
                    var extension = Path.GetExtension(file.FileName);

                    //update the image - Check if there is an image
                    if (obj.ImgUrl != null)
                    {
                        //old image path
                        var oldImagePath = Path.Combine(wwwRootPath, obj.ImgUrl.TrimStart('\\'));
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
                    obj.ImgUrl = @"\images\activites\" + FileName + extension;
                }
                if (obj.Id == 0)
                {
                    _UnitOfWork.Activite.Add(obj);
                    TempData["success"] = "Activité ajouté avec succès";

                }
                else
                {
                    _UnitOfWork.Activite.Update(obj);
                    TempData["success"] = "Activité modifié avec succès";
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
            var ActiviteList = _UnitOfWork.Activite.GetAll();
            return Json(new { data = ActiviteList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Activite.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, Obj.ImgUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _UnitOfWork.Activite.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Activité supprimé avec succès" });

        }
        #endregion
    }
}

