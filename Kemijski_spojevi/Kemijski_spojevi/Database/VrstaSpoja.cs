using System;
using System.Collections.Generic;

namespace Kemijski_spojevi.Database
{
    public partial class VrstaSpoja
    {
        public VrstaSpoja()
        {
            Spoj = new HashSet<Spoj>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Spoj> Spoj { get; set; }
    }
}
