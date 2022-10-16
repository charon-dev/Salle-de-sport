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
    public class Chariot
    {
        public int Id { get; set; }
        public int AbonnementId { get; set; }
        [ForeignKey("AbonnementId")]
        [ValidateNever]
        public Abonnement Abonnement { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double Prix { get; set; }
    }
}
