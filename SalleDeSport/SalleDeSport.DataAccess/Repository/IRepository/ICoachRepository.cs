using SalleDeSport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.DataAccess.Repository.IRepository
{
    public interface ICoachRepository : IRepository<Coach>
    {
        void Update(Coach obj);
    }
}
