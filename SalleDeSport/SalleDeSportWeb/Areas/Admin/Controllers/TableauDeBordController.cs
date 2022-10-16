using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Models.ViewModels;
using SalleDeSport.Utility;
using System.Security.Claims;

namespace SalleDeSportWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class TableauDeBordController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TableauDeBordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork; 
        }
        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<Commande> Commandes;
            //IEnumerable<DetailleCommande> DetailleCommandes = _unitOfWork.DetailleCommande.GetAll(includeProperties: "Abonnement");

            if (User.IsInRole(SD.Role_Admin))
            {
                Commandes = _unitOfWork.Commande.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                var claimsIDentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
                Commandes = _unitOfWork.Commande.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "ApplicationUser");
                //foreach (var Commande in Commandes)
                //{
                //    DetailleCommandes = _unitOfWork.DetailleCommande.GetAll(u => u.IdCommande == Commande.Id, includeProperties: "Abonnement");
                //}

            }

            return Json(new { data = Commandes /*, DetailleCommandes*/ });
        }
        #endregion
    }
}
