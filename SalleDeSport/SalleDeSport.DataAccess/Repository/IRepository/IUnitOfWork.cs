using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICoachRepository Coach { get; }
        IAbonnementRepository Abonnement { get; }
        IChariotRepository Chariot { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IActiviteRepository Activite { get; }
        ICommandeRepository Commande { get; }
        IDetailleCommandeRepository DetailleCommande { get; }
        void Save();
    }
}
