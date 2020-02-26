using System;
using System.Collections.Generic;

namespace BoVoyageFinal.Models
{
    public partial class Voyageur
    {
        public int Id { get; set; }
        public int Idvoyage { get; set; }

        public virtual Personne IdNavigation { get; set; }
        public virtual Voyage IdvoyageNavigation { get; set; }
    }
}
