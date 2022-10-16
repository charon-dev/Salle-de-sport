using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models.ViewModels
{
    public class ChariotVM
    {
        public IEnumerable<Chariot> Chariots { get; set; }
        public Commande Commande { get; set; }
    }
}
