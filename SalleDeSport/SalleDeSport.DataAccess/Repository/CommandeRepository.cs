using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSport.Models;
using SalleDeSportWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository
{
    public class CommandeRepository : Repository<Commande>, ICommandeRepository
    {
        private ApplicationDbContext _db;
        public CommandeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Commande obj)
        {
            _db.Commande.Update(obj);
        }
        public void UpdateStatus(int id, string orderStatus, string? payementStatus = null)
        {
            var Commande = _db.Commande.FirstOrDefault(u => u.Id == id);
            if (Commande != null)
            {
                //Update Order status
                Commande.StatutDeCommande = orderStatus;
                if (payementStatus != null)
                {
                    //Update Payment status
                    Commande.StatutDePayement = payementStatus;
                }
            }
        }
        public void UpdateStripePayementID(int id, string sessionId, string payementItentId)
        {
            var Commande = _db.Commande.FirstOrDefault(u => u.Id == id);
            Commande.DatePayement = DateTime.Now;
            Commande.DateDebutAbonnement = DateTime.Now;
            //Date fin abo
            var DetailleCommande = _db.DetailleCommande.FirstOrDefault(u => u.IdCommande == Commande.Id);
            if (DetailleCommande.AbonnementId == 1)
            {
                Commande.DateFintAbonnement = Commande.DateDebutAbonnement.AddDays(30);
            }
            else {
                if (DetailleCommande.AbonnementId == 2) {
                    Commande.DateFintAbonnement = Commande.DateDebutAbonnement.AddDays(180);
                }
                else
                    Commande.DateFintAbonnement = Commande.DateDebutAbonnement.AddDays(365);
            }
            Commande.SessionId = sessionId;
            Commande.PayementIntentId = payementItentId;

        }
    }
}
