using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models
{
    public class Activite
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        [ValidateNever]
        public string ImgUrl { get; set; }
        
    }
}
