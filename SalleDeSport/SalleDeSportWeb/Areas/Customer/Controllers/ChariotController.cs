using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSport.Models.ViewModels;
using SalleDeSport.Utility;
using Stripe.Checkout;
using System.Security.Claims;

namespace SalleDeSportWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class ChariotController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ChariotVM ChariotVM { get; set; }
        public ChariotController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }   
        
        public IActionResult Index()
        {
            var claimsIDentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIDentity.FindFirst(ClaimTypes.NameIdentifier);
            ChariotVM = new ChariotVM()
            {
                Chariots = _unitOfWork.Chariot.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Abonnement"),
                Commande = new()
            };
            foreach (var chariot in ChariotVM.Chariots)
            {
                chariot.Prix = chariot.Abonnement.Prix;
                ChariotVM.Commande.PrixTotal = chariot.Prix;
            }
            return View(ChariotVM);
        }

        public IActionResult SuppChariot(int ChariotId)
        {
            var ChariotaSupprime = _unitOfWork.Chariot.GetFirstOrDefault(u => u.Id == ChariotId);
            _unitOfWork.Chariot.Remove(ChariotaSupprime);
            _unitOfWork.Save();
            var count = _unitOfWork.Chariot.GetAll(u => u.ApplicationUserId == ChariotaSupprime.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            TempData["success"] = "Abonnement supprimé du chariot";
            return RedirectToAction("Index");
        }
        
        //GET
        public IActionResult ConfirmationPayement()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ChariotVM = new ChariotVM()
            {
                Chariots = _unitOfWork.Chariot.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Abonnement"),
                Commande = new()
            };

            ChariotVM.Commande.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

            ChariotVM.Commande.Nom = ChariotVM.Commande.ApplicationUser.Nom;
            ChariotVM.Commande.Prenom = ChariotVM.Commande.ApplicationUser.Prenom;
            ChariotVM.Commande.Nationalite = ChariotVM.Commande.ApplicationUser.Nationalite;
            ChariotVM.Commande.CIN = ChariotVM.Commande.ApplicationUser.CIN;

            foreach (var chariot in ChariotVM.Chariots)
            {
                chariot.Prix = chariot.Abonnement.Prix;
                ChariotVM.Commande.PrixTotal = chariot.Prix;
            }
            return View(ChariotVM);
        }
        
        //POST
        [HttpPost]
        [ActionName("ConfirmationPayement")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmationPayementPOST()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ChariotVM.Chariots = _unitOfWork.Chariot.GetAll(u => u.ApplicationUserId == claim.Value, includeProperties: "Abonnement");

            ChariotVM.Commande.StatutDePayement = SD.PayementStatusEnattente;
            ChariotVM.Commande.StatutDeCommande = SD.StatusEnAttente;
            ChariotVM.Commande.ApplicationUserId = claim.Value;

            foreach (var chariot in ChariotVM.Chariots)
            {
                chariot.Prix = chariot.Abonnement.Prix;
                ChariotVM.Commande.PrixTotal = chariot.Prix;
            }

            _unitOfWork.Commande.Add(ChariotVM.Commande);
            _unitOfWork.Save();

            foreach (var chariot in ChariotVM.Chariots)
            {
                DetailleCommande DetailleCommande = new()
                {
                    AbonnementId = chariot.AbonnementId,
                    IdCommande = ChariotVM.Commande.Id,
                    Prix = chariot.Prix
                };
                _unitOfWork.DetailleCommande.Add(DetailleCommande);
                _unitOfWork.Save();
            }
            
            //Stripe settings
            var domain = "https://localhost:44334/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"customer/chariot/PayementConfirme?id={ChariotVM.Commande.Id}",
                CancelUrl = domain + "$customer/chariot/Index",
            };
            foreach (var item in ChariotVM.Chariots)
            {
                var sessionLineItem = new SessionLineItemOptions
                {

                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Prix * 100),
                        Currency = "mad",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Abonnement.Nom,
                        },

                    },
                    Quantity = 1,

                };
                options.LineItems.Add(sessionLineItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);

            _unitOfWork.Commande.UpdateStripePayementID(ChariotVM.Commande.Id, session.Id, session.PaymentIntentId);
            _unitOfWork.Save();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult PayementConfirme(int id)
        {
            Commande Commande = _unitOfWork.Commande.GetFirstOrDefault(u => u.Id == id, includeProperties: "ApplicationUser");
            var service = new SessionService();
            Session session = service.Get(Commande.SessionId);
            //Check stripe status
            if (session.PaymentStatus.ToLower() == "paid")
            {
                _unitOfWork.Commande.UpdateStatus(id, SD.StatusApprouve, SD.PayementStatusApprouve);
                _unitOfWork.Save();
            }
            //Clear the shopping cart
            Chariot Chariot = _unitOfWork.Chariot.GetFirstOrDefault(u => u.ApplicationUserId == Commande.ApplicationUserId);
            _unitOfWork.Chariot.Remove(Chariot);
            _unitOfWork.Save();
            return View(id);
        }

    }
}
