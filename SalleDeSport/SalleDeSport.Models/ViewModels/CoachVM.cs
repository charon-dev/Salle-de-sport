using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalleDeSport.Models.ViewModels
{
    public class CoachVM
    {
        public Coach Coach { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> ListDesActivites { get; set; }
    }
}
