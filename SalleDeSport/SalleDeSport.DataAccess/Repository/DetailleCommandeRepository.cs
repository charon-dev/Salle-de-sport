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
    public class DetailleCommandeRepository : Repository<DetailleCommande>, IDetailleCommandeRepository
    {
        private ApplicationDbContext _db;
        public DetailleCommandeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DetailleCommande obj)
        {
            _db.DetailleCommande.Update(obj);
        }
    }
}
