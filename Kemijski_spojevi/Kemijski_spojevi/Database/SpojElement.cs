using System;
using System.Collections.Generic;

namespace Kemijski_spojevi.Database
{
    public partial class SpojElement
    {
        public int SpojId { get; set; }
        public int ElementId { get; set; }
        public int Count { get; set; }

        public Element Element { get; set; }
        public Spoj Spoj { get; set; }
    }
}
