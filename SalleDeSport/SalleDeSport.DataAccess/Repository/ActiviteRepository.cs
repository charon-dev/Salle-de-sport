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
    public class ActiviteRepository : Repository<Activite>, IActiviteRepository
    {
        private ApplicationDbContext _db;
        public ActiviteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Activite obj)
        {
            _db.activites.Update(obj);
        }
    }
}
