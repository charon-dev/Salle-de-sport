using Microsoft.AspNetCore.Identity;
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
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public int Age { get; set; }
        [DisplayName("Nationalité")]
        public string Nationalite { get; set; }
        public string CIN { get; set; }

        

        
    }
}
