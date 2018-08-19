using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kemijski_spojevi.Database
{
    public partial class Spoj
    {
        public Spoj()
        {
            SpojElement = new HashSet<SpojElement>();
        }

        public int Id { get; set; }
        [Display(Name = "Naziv spoja")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Vrsta spoja")]
        [Required]
        public int TypeId { get; set; }

        public VrstaSpoja Type { get; set; }
        [Display(Name = "Elementi")]
        public ICollection<SpojElement> SpojElement { get; set; }

        public const int MinSizeOfElements = 2;
        
    }
}
