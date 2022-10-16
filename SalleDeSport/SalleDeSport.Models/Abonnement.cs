using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models
{
    public class Abonnement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Description { get; set; }
        [DisplayName("Durée")]
        [Required]
        public string Duree { get; set; }
        [Required]
        public int Prix { get; set; }
    }
}
