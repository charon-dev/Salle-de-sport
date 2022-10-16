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
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DisplayName("Numero Téléphone")]
        public string NumeroTelephone { get; set; }
        [Required]
        [DisplayName("Adresse Email")]
        public string AdresseEmail { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        [DisplayName("Activité")]
        public int ActiviteId { get; set; }
        [ValidateNever]
        [ForeignKey("ActiviteId")]
        public Activite Activite { get; set; }
    }
}
