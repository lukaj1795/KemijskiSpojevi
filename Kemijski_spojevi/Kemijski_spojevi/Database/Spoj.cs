using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemijski_spojevi.Database
{
    public partial class Spoj
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public virtual VrstaSpoja VrstaSpoja { get; set; }

        public virtual ICollection<SpojElement> SpojElement { get; set; }

    }
}
