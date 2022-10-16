using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Utility;

namespace SalleDeSportWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class AbonnementController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public AbonnementController(IUnitOfWork UnitOfWork)
        {
            _UnitOfWork = UnitOfWork;        
        }

        public IActionResult Index()
        {
            return View();
        }

        //Get
        public IActionResult Upsert(int? Id)
        {
            Abonnement abonnement = new();
            if (Id == null || Id == 0)
            {
                //Create brand
                return View(abonnement);
            }
            else
            {
                //Update product
                abonnement = _UnitOfWork.Abonnement.GetFirstOrDefault(u => u.Id == Id);
                return View(abonnement);
            }
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Abonnement obj)
        {

            if (ModelState.IsValid)
            {             
                if (obj.Id == 0)
                {
                    _UnitOfWork.Abonnement.Add(obj);
                    TempData["success"] = "Abonnement créé avec succès";
                }
                else
                {
                    _UnitOfWork.Abonnement.Update(obj);
                    TempData["success"] = "Abonnement modifié avec succès";
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
            var AbonnementList = _UnitOfWork.Abonnement.GetAll();
            return Json(new { data = AbonnementList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? Id)
        {
            var Obj = _UnitOfWork.Abonnement.GetFirstOrDefault(u => u.Id == Id);
            if (Obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _UnitOfWork.Abonnement.Remove(Obj);
            _UnitOfWork.Save();
            return Json(new { success = true, message = "Abonnement supprimé avec succès" });

        }
        #endregion
    }
}
