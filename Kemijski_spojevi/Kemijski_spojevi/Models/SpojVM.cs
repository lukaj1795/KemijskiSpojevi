using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kemijski_spojevi.Models
{
    public class SpojVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Vrsta spoja")]
        public int TypeID { get; set; }
        [Display(Name = "Elementi")]
        public ICollection<int> Elements { get; set; }
    }
}
