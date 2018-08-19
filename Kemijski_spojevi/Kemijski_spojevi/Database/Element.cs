using System;
using System.Collections.Generic;

namespace Kemijski_spojevi.Database
{
    public partial class Element
    {
        public Element()
        {
            SpojElement = new HashSet<SpojElement>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }

        public ICollection<SpojElement> SpojElement { get; set; }
    }
}
