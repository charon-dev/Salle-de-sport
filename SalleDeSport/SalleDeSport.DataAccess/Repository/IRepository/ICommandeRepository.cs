using SalleDeSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository.IRepository
{
    public interface ICommandeRepository : IRepository<Commande>
    {
        void Update(Commande obj);
        void UpdateStatus(int id, string orderStatus, string? payementStatus = null);
        public void UpdateStripePayementID(int id, string sessionId, string payementItentId);
    }
}
