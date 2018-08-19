using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kemijski_spojevi.Database;

namespace Kemijski_spojevi.Models
{
    public class SpojAPIModel
    {
        public int Id;

        public string Name;

        public string TypeName;

        public IEnumerable<SpojElementAPIModel> ElementCounts;


    }
}
