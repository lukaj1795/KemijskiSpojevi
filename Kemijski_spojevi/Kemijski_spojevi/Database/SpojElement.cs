using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kemijski_spojevi.Database
{
    public partial class SpojElement
    {
        public int Id { get; set; }

        public int SpojId { get; set; }

        public int ElementId { get; set; }

        public int Count { get; set; }
    }
}
