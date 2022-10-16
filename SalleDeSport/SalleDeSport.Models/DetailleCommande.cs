using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models
{
    public class DetailleCommande
    {
        public int Id { get; set; }
        public double Prix { get; set; }
        [Required]
        public int AbonnementId { get; set; }
        [ForeignKey("AbonnementId")]
        [ValidateNever]
        public Abonnement Abonnement { get; set; }
        [Required]
        public int IdCommande { get; set; }
        [ForeignKey("IdCommande")]
        [ValidateNever]
        public Commande Commande { get; set; }
    }
}
