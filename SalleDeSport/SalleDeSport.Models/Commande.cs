using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models
{
    public class Commande
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Date debut d'bonnement")]
        public DateTime DateDebutAbonnement { get; set; }
        [DisplayName("Date fin d'abonnement")]
        [Required]
        public DateTime DateFintAbonnement { get; set; }
        public double PrixTotal { get; set; }
        public string? StatutDePayement { get; set; }
        public string? StatutDeCommande { get; set; }
        public string? SessionId { get; set; }
        public string? PayementIntentId { get; set; }
        public DateTime DatePayement { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string CIN { get; set; }
        [DisplayName("Nationalité")]
        public string Nationalite { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
