using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Models.ViewModels;
using SalleDeSportWeb.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SalleDeSportWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new()
            {
                ActiviteList = _unitOfWork.Activite.GetAll(),
                coachNatation = _unitOfWork.Coach.GetFirstOrDefault(u=>u.Nom=="Sebti",includeProperties: "Activite"),
                coachBoxe = _unitOfWork.Coach.GetFirstOrDefault(u => u.Nom == "Bongo", includeProperties: "Activite"),
                coachMusculation = _unitOfWork.Coach.GetFirstOrDefault(u => u.Nom == "Belle", includeProperties: "Activite"),
                AbonnementList = _unitOfWork.Abonnement.GetAll()
            };
            return View(homeVM);
        }
       //GET
        public IActionResult AjoutPanier(int AboId)
        {
            Chariot ChariotObj = new()
            {
                AbonnementId = AboId,
                Abonnement = _unitOfWork.Abonnement.GetFirstOrDefault(u => u.Id == AboId)
            };
            return View(ChariotObj);

        }
        //Post
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize]
        public IActionResult AjoutPanier(Chariot chariot)
        {
            var ClaimIdentity = (ClaimsIdentity)User.Identity;
            var claim = ClaimIdentity.FindFirst(ClaimTypes.NameIdentifier);
       
            if(_unitOfWork.Commande.GetAll(u => u.ApplicationUserId == claim.Value).Count() == 0)
            {
                if(_unitOfWork.Chariot.GetAll(u=>u.ApplicationUserId== claim.Value).Count() == 0) 
                {
                    chariot.ApplicationUserId = claim.Value;

                    _unitOfWork.Chariot.Add(chariot);
                    _unitOfWork.Save();
                    TempData["Success"] = "Abonnement ajouté avec succès dans votre panier";
                }
                else
                {
                    TempData["error"] = "Payez votre abonnement";
                }
                
            }
            else
            {
                var commande = _unitOfWork.Commande.GetAll(u => u.ApplicationUserId == claim.Value).Last();
                if (commande.DateFintAbonnement >= System.DateTime.Now)
                {
                    TempData["error"] = "Vous avez deja un abonnement";
                }
            }
           
            return RedirectToAction(nameof(Index));
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