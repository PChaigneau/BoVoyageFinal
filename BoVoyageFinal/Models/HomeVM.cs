using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyageFinal.Models
{
    public class HomeVM
    {
        public List<Voyage> Top5Prix { get; set; }
        public List<Voyage> Top5DateDepart { get; set; }
        public List<Destination> Top5Destination { get; set; }
    }
}
