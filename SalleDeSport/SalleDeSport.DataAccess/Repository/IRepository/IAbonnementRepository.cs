using SalleDeSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository.IRepository
{
    public interface IAbonnementRepository : IRepository<Abonnement>
    {
        void Update(Abonnement obj);
    }
}
