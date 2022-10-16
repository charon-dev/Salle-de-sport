using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Activite> ActiviteList { get; set; }
        //public IEnumerable<Coach> CoachList { get; set; }
        public Coach coachNatation { get; set; }
        public Coach coachBoxe { get; set; }
        public Coach coachMusculation { get; set; }
        public IEnumerable<Abonnement> AbonnementList { get; set; }
    }
}
