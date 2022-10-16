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
    public class AbonnementRepository : Repository<Abonnement>, IAbonnementRepository
    {
        private ApplicationDbContext _db;
        public AbonnementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Abonnement obj)
        {
            _db.abonnements.Update(obj);
        }
    }
}
