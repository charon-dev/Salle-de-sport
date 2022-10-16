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
    public class ChariotRepository : Repository<Chariot>, IChariotRepository
    {
        private ApplicationDbContext _db;
        public ChariotRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        
    }
}
