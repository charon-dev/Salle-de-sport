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
    public class CoachRepository : Repository<Coach>, ICoachRepository
    {
        private ApplicationDbContext _db;
        public CoachRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Coach obj)
        {
            _db.coachs.Update(obj);
        }
    }
}
