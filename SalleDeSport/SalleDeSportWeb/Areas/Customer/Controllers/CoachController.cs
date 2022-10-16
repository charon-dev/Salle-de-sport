using Microsoft.AspNetCore.Mvc;
using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;

namespace SalleDeSportWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CoachController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoachController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<Coach> ListCoach = _unitOfWork.Coach.GetAll(includeProperties: "Activite");
            return View(ListCoach);
        }
    }
}
