using SalleDeSport.DataAccess.Repository.IRepository;
using SalleDeSportWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICoachRepository Coach { get; private set; }
        public IAbonnementRepository Abonnement { get; private set; }
        public IChariotRepository Chariot { get; private set; }
        public IActiviteRepository Activite { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public ICommandeRepository Commande { get; private set; }
        public IDetailleCommandeRepository DetailleCommande { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Coach = new CoachRepository(_db);
            Abonnement = new AbonnementRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Chariot = new ChariotRepository(_db);
            Activite=new ActiviteRepository(_db);
            Commande = new CommandeRepository(_db);
            DetailleCommande = new DetailleCommandeRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
